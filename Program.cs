using xml_file.Models;
using xml_file.Services;

string filePath = "Data/XmlFile.xml";

List<Customer> writeCustomers =
[
    new() { Name = "Peter Parker", Email = "peter.parker@marvel.com" },
    new() { Name = "Ben Parker", Email = "ben.parker@marvel.com" },
    new() { Name = "Mary Jane", Email = "mary.jane@marvel.com" }
];

XmlFileService.WriteDataToXmlFile(filePath, writeCustomers);

var readCustomers = XmlFileService.ReadDataFromXmlFile(filePath);

Console.ForegroundColor = ConsoleColor.Magenta;
foreach (var currentCustomer in readCustomers)
{
    Console.WriteLine($"{ currentCustomer.Id } - { currentCustomer.Name } - { currentCustomer.Email }");
}