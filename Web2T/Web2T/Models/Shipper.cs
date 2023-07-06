using System;
using System.Collections.Generic;

namespace Web2T.Models;

public partial class Shipper
{
    public int ShipperId { get; set; }

    public string? ShipperName { get; set; }

    public string? Phone { get; set; }

    public string? Company { get; set; }

    public string? ShipDate { get; set; }
}
