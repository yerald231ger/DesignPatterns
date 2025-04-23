using PaymentMethodDescriminator.Domain.Entities;
using PaymentMethodDescriminator.Domain.Repositories;

namespace PaymentMethodDescriminator.Approaches.RuleBasedApproach.Rules;

public class ProductCategoryPaymentRule : IAsyncPaymentMethodRule, IPaymentMethodSpecification
{
    private readonly ICategoryPaymentRuleRepository _repository;
    private readonly Dictionary<string, HashSet<string>> _categoryRules;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private DateTime _lastRefresh = DateTime.MinValue;
    private readonly TimeSpan _cacheTimeout = TimeSpan.FromMinutes(5);

    public ProductCategoryPaymentRule(ICategoryPaymentRuleRepository repository)
    {
        _repository = repository;
        _categoryRules = new Dictionary<string, HashSet<string>>();
    }

    private async Task RefreshRulesIfNeededAsync()
    {
        if (DateTime.UtcNow - _lastRefresh < _cacheTimeout) return;

        await _lock.WaitAsync();
        try
        {
            if (DateTime.UtcNow - _lastRefresh < _cacheTimeout) return;

            var rules = await _repository.GetAllActiveRulesAsync();
            var newRules = new Dictionary<string, HashSet<string>>();

            foreach (var rule in rules)
            {
                if (!newRules.ContainsKey(rule.Category.Name))
                {
                    newRules[rule.Category.Name] = new HashSet<string>();
                }
                newRules[rule.Category.Name].Add(rule.PaymentMethod.MethodType);
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
        return _categoryRules.TryGetValue(product.Category.Name, out var allowedMethods) &&
               allowedMethods.Contains(paymentMethod.MethodType);
    }

    public bool CanUsePaymentMethod(Product product, PaymentMethod paymentMethod)
    {
        // Force sync over async, not recommended for production
        return CanUsePaymentMethodAsync(product, paymentMethod).GetAwaiter().GetResult();
    }

    public IPaymentMethodSpecification And(IPaymentMethodSpecification other)
    {
        return new AndSpecification(this, other);
    }

    public IPaymentMethodSpecification Or(IPaymentMethodSpecification other)
    {
        return new OrSpecification(this, other);
    }
} 