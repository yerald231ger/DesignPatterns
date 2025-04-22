using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Enums;
using PaymentMethodDescriminator.Domain.Repositories;

namespace PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;

public class ProductCategoryPaymentRule : IAsyncPaymentMethodRule
{
    private readonly ICategoryPaymentRuleRepository _repository;
    private readonly Dictionary<string, HashSet<PaymentMethodType>> _categoryRules;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private DateTime _lastRefresh = DateTime.MinValue;
    private readonly TimeSpan _cacheTimeout = TimeSpan.FromMinutes(5);

    public ProductCategoryPaymentRule(ICategoryPaymentRuleRepository repository)
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
                if (!newRules.ContainsKey(rule.Category))
                {
                    newRules[rule.Category] = new HashSet<PaymentMethodType>();
                }
                newRules[rule.Category].Add(rule.PaymentMethodType);
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

    public async Task<bool> CanUsePaymentMethodAsync(Product product, PaymentMethod paymentMethod)
    {
        await RefreshRulesIfNeededAsync();
        return _categoryRules.TryGetValue(product.Category, out var allowedMethods) &&
               allowedMethods.Contains(paymentMethod.Type);
    }

    public bool CanUsePaymentMethod(Product product, PaymentMethod paymentMethod)
    {
        // Force sync over async, not recommended for production
        return CanUsePaymentMethodAsync(product, paymentMethod).GetAwaiter().GetResult();
    }
} 