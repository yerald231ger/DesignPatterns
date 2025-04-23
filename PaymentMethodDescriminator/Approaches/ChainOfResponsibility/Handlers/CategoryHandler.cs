using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Repositories;
using PaymentMethodDescriminator.Domain.Enums;

namespace PaymentMethodDescriminator.Approaches.ChainOfResponsibility.Handlers;

public class CategoryHandler : AbstractPaymentMethodHandler
{
    private readonly ICategoryPaymentRuleRepository _repository;
    private readonly Dictionary<string, HashSet<PaymentMethodType>> _categoryRules;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private DateTime _lastRefresh = DateTime.MinValue;
    private readonly TimeSpan _cacheTimeout = TimeSpan.FromMinutes(5);

    public CategoryHandler(ICategoryPaymentRuleRepository repository)
    {
        _repository = repository;
        _categoryRules = new Dictionary<string, HashSet<PaymentMethodType>>();
    }

    private async Task RefreshRulesIfNeededAsync()
    {
        if (DateTime.UtcNow - _lastRefresh < _cacheTimeout) return;

        await _lock.WaitAsync();
        try
        {
            if (DateTime.UtcNow - _lastRefresh < _cacheTimeout) return;

            var rules = await _repository.GetAllActiveRulesAsync();
            var newRules = new Dictionary<string, HashSet<PaymentMethodType>>();

            foreach (var rule in rules)
            {
                if (!newRules.ContainsKey(rule.Category.Name))
                {
                    newRules[rule.Category.Name] = new HashSet<PaymentMethodType>();
                }
                newRules[rule.Category.Name].Add(rule.PaymentMethod.Type);
            }

            _categoryRules.Clear();
            foreach (var kvp in newRules)
            {
                _categoryRules[kvp.Key] = kvp.Value;
            }
            _lastRefresh = DateTime.UtcNow;
        }
        finally
        {
            _lock.Release();
        }
    }

    public override bool Handle(Product product, PaymentMethod paymentMethod)
    {
        RefreshRulesIfNeededAsync().GetAwaiter().GetResult();
        
        if (_categoryRules.TryGetValue(product.Category.Name, out var allowedMethods) &&
            allowedMethods.Contains(paymentMethod.Type))
        {
            return base.Handle(product, paymentMethod);
        }

        return false;
    }
} 