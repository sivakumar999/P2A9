using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Management System");
            Console.WriteLine("-----------------------");

            Console.Write("Enter CustomerId: ");
            int orderId = int.Parse(Console.ReadLine());

            Console.Write("Enter CustomerId: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Enter TotalAmount: ");
            decimal totalAmount = decimal.Parse(Console.ReadLine());

            using (var context = new OrderDBEntities()) // Replace with your actual context name
            {
                try
                {
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);

                    if (customer == null)
                    {
                        Console.WriteLine($"Customer with ID {customerId} not found.");
                        return;
                    }

                    var order = new Order
                    {
                        OrderId = orderId,
                        CustomerId = customerId,
                        OrderDate = DateTime.Now,
                        TotalAmount = totalAmount
                    };

                    context.Orders.Add(order);

                    // Update the customer's total spending
                    customer.TotalSpending += totalAmount;

                    context.SaveChanges();

                    Console.WriteLine("Order placed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
