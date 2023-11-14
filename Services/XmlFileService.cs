using System.Xml;
using xml_file.Models;

namespace xml_file.Services;

public static class XmlFileService
{
    public static void WriteDataToXmlFile(string filePath, List<Customer> customers)
    {

        XmlDocument xmlDoc = new();
        XmlElement root = xmlDoc.CreateElement("Customers");
        xmlDoc.AppendChild(root);

        foreach (var currentCustomer in customers)
        {
            XmlElement elementCustomer = CreateCustomerXml(xmlDoc, currentCustomer);
            root.AppendChild(elementCustomer);
        }

        xmlDoc.Save(filePath);
    }

    public static List<Customer> ReadDataFromXmlFile(string filePath)
    {
        List<Customer> customers = [];

        if (File.Exists(filePath))
        {
            XmlDocument xmlDoc = new();
            xmlDoc.Load(filePath);

            XmlNodeList nodeListCustomers = xmlDoc.GetElementsByTagName("Customer");

            foreach (XmlNode nodeCurrentCustomer in nodeListCustomers)
            {
                string? id = nodeCurrentCustomer.SelectSingleNode("Id")?.InnerText;

                if (Guid.TryParse(id, out Guid guidId))
                {
                    customers.Add(new Customer
                    {
                        Id = guidId,
                        Name = nodeCurrentCustomer.SelectSingleNode("Name")?.InnerText ?? string.Empty,
                        Email = nodeCurrentCustomer.SelectSingleNode("Email")?.InnerText ?? string.Empty,
                    });
                }
            }
        }

        return customers;
    }

    private static XmlElement CreateCustomerXml(XmlDocument xmlDoc, Customer customer)
    {
        XmlElement elementCustomer = xmlDoc.CreateElement("Customer");
        
        XmlElement elementId = xmlDoc.CreateElement("Id");
        elementId.InnerText = customer.Id.ToString();
        
        XmlElement elementName = xmlDoc.CreateElement("Name");
        elementName.InnerText = customer.Name;
        
        XmlElement elementEmail = xmlDoc.CreateElement("Email");
        elementEmail.InnerText = customer.Email;
        
        elementCustomer.AppendChild(elementId);
        elementCustomer.AppendChild(elementName);
        elementCustomer.AppendChild(elementEmail);
        
        return elementCustomer;
    }
}