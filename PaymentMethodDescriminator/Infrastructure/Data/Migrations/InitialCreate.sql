-- Categories Table
CREATE TABLE Categories (
    CategoryId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    IsActive INTEGER NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME
);

-- Payment Methods Table
CREATE TABLE PaymentMethods (
    PaymentMethodId INTEGER PRIMARY KEY,
    Name TEXT NOT NULL,
    MethodType TEXT NOT NULL,
    IsActive INTEGER NOT NULL DEFAULT 1,
    IsElectronicTransaction INTEGER NOT NULL DEFAULT 0,
    Priority INTEGER,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME
);

-- Products Table
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY,
    Name TEXT NOT NULL,
    Description TEXT,
    CategoryId INTEGER NOT NULL,
    UnitPrice DECIMAL(10,2),
    IsActive INTEGER NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

-- Category Payment Method Rules Table
CREATE TABLE CategoryPaymentMethodRules (
    RuleId INTEGER PRIMARY KEY AUTOINCREMENT,
    CategoryId INTEGER NOT NULL,
    PaymentMethodId INTEGER NOT NULL,
    MinAmount DECIMAL(10,2),
    MaxAmount DECIMAL(10,2),
    IsActive INTEGER NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethods(PaymentMethodId)
);

-- Insert some initial data
INSERT INTO Categories (Name) VALUES 
('Food'),
('Electronics'),
('Fuel'),
('Services');

INSERT INTO PaymentMethods (PaymentMethodId, Name, MethodType, IsElectronicTransaction, Priority) VALUES 
(1, 'Cash', 'CASH', 0, 1),
(2, 'Credit Card', 'CARD', 1, 2),
(3, 'Debit Card', 'CARD', 1, 2),
(4, 'Food Voucher', 'VOUCHER', 0, 3);

-- Set up initial rules
INSERT INTO CategoryPaymentMethodRules (CategoryId, PaymentMethodId, MinAmount, MaxAmount) VALUES 
(1, 1, 0, 1000),    -- Food can be paid with cash up to 1000
(1, 4, 0, NULL),    -- Food can be paid with food vouchers (no limit)
(2, 2, 0, NULL),    -- Electronics can be paid with credit card (no limit)
(2, 3, 0, 5000),    -- Electronics can be paid with debit card up to 5000
(3, 1, 0, 500),     -- Fuel can be paid with cash up to 500
(3, 2, 0, NULL),    -- Fuel can be paid with credit card (no limit)
(3, 3, 0, NULL);    -- Fuel can be paid with debit card (no limit) 