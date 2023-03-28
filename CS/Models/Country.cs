using System;
using System.Collections.Generic;

namespace CascadingComboBoxes.Models;

public partial class Country {
    public int CountryId { get; set; }
    public string? CountryName { get; set; }
    public int? CapitalId { get; set; }
    public virtual City? Capital { get; set; }
    public virtual ICollection<City> Cities { get; } = new List<City>();
}
