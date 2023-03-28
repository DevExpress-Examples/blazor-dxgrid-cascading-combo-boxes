using System;
using System.Collections.Generic;

namespace CascadingComboBoxes.Models;

public partial class City {
    public int CityId { get; set; }
    public string? CityName { get; set; }
    public int? CountryId { get; set; }
    public virtual ICollection<Country> Countries { get; } = new List<Country>();
    public virtual Country? Country { get; set; }
}
