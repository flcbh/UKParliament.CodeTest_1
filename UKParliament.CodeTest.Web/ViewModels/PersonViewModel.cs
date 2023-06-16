using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Web.ViewModels;

public class PersonViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string PostCode { get; set; }

    public string City { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }
}