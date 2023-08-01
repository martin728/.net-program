using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(customer => customer.Orders.Sum(order => order.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.Select(customer => (
                customer: customer,
                suppliers: suppliers.Where(supplier =>
                    supplier.Country.Equals(customer.Country, StringComparison.OrdinalIgnoreCase) &&
                    supplier.City.Equals(customer.City, StringComparison.OrdinalIgnoreCase)
                )
            ));        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.GroupJoin(suppliers,
                customer => new { customer.Country, customer.City },
                supplier => new { supplier.Country, supplier.City },
                (customer, supplierGroup) => (customer, suppliers: supplierGroup)
            );        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            if (limit < 0)
                limit = 0;

            return customers.Where(customer => customer.Orders.Sum(order => order.Total) > limit);
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(IEnumerable<Customer> customers)
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            return customers
                .Select<Customer, (Customer customer, DateTime? dateOfEntry)>(customer =>
                {
                    DateTime? minDate = null;
                    foreach (var order in customer.Orders)
                    {
                        if (minDate == null || order.OrderDate < minDate)
                            minDate = order.OrderDate;
                    }
                    return (customer, minDate);
                })
                .Where(tuple => tuple.dateOfEntry.HasValue)
                .Select(tuple => (tuple.customer, tuple.dateOfEntry.Value));
        }


        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(IEnumerable<Customer> customers)
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            return customers
                .Where(c => c.Orders != null && c.Orders.Any())
                .Select(c => (customer: c, dateOfEntry: c.Orders.Min(o => o.OrderDate)))
                .OrderBy(ct => ct.dateOfEntry);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            return customers.Where(customer =>
                !string.IsNullOrEmpty(customer.PostalCode) && customer.PostalCode.Any(c => !char.IsDigit(c)) ||
                string.IsNullOrEmpty(customer.Region) ||
                !customer.Phone.Contains('(') && !customer.Phone.Contains(')')
            );        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            return products.GroupBy(product => product.Category)
                .Select(categoryGroup => new Linq7CategoryGroup
                {
                    Category = categoryGroup.Key,
                    UnitsInStockGroup = categoryGroup.GroupBy(product => product.UnitsInStock)
                        .Select(unitsGroup => new Linq7UnitsInStockGroup
                        {
                            UnitsInStock = unitsGroup.Key,
                            Prices = unitsGroup.OrderBy(product => product.UnitPrice)
                                .Select(product => product.UnitPrice)
                                .ToList()
                        })
                        .OrderBy(group => group.UnitsInStock)
                        .ToList()
                })
                .OrderBy(categoryGroup => categoryGroup.Category);
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            var cheapProducts = products.Where(p => p.UnitPrice <= cheap);
            var middleProducts = products.Where(p => p.UnitPrice > cheap && p.UnitPrice <= middle);
            var expensiveProducts = products.Where(p => p.UnitPrice > middle && p.UnitPrice <= expensive);

            return new[]
            {
                (category: cheap, products: cheapProducts),
                (category: middle, products: middleProducts),
                (category: expensive, products: expensiveProducts)
            };
        }


        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            return customers.GroupBy(customer => customer.City)
                .Select(cityGroup => (
                    city: cityGroup.Key,
                    averageIncome: (int)cityGroup.Average(customer => customer.Orders.Sum(order => order.Total)),
                    averageIntensity: (int)cityGroup.Average(customer => customer.Orders.Count())
                ));        
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var uniqueCountryNames = suppliers.Select(supplier => supplier.Country).Distinct();
            var sortedUniqueCountryNames = uniqueCountryNames.OrderBy(country => country.Length).ThenBy(country => country);
            return string.Join("", sortedUniqueCountryNames);        
        }
    }
}