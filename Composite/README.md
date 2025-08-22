# Composite Pattern - Amount Calculator Tree

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │ uses
         ↓
┌──────────────────┐
│IAmountCalculator │←─────────────────┐ implements
│     Leaf         │                  │
├──────────────────┤                  │
│ +Calculate()     │                  │
└──────────────────┘                  │
         ↑ implements                 │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│   LeafNode       │      │  CompositeNode   │
│   (Product)      │      │    (Category)    │
├──────────────────┤      ├──────────────────┤
│ +Calculate()     │      │ +Calculate()     │
│ -amount          │      │ +Add(leaf)       │
└──────────────────┘      │ +Remove(leaf)    │
                          │ -children[]      │
                          └──────────────────┘
                                   │ contains
                                   ↓
                          ┌──────────────────┐
                          │   LeafNode       │
                          │   (Product)      │
                          ├──────────────────┤
                          │ +Calculate()     │
                          └──────────────────┘
```

## Tree Structure Example:
```
         Category (Root)
           /    │    \
    Product1  Product2  SubCategory
                            │
                        Product3
```

## Explanation:
1. **IAmountCalculatorLeaf**: Common interface for leaf and composite objects
2. **LeafNode**: Individual products with amounts
3. **CompositeNode**: Categories containing other products/categories
4. **Client**: Works with tree structure uniformly

## Key Benefit:
Treats individual products and categories uniformly, enabling recursive calculation of totals across complex hierarchies.