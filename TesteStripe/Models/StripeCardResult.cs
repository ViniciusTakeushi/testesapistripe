﻿namespace TesteStripe.Models
{
    public class StripeCard
    {
        public string id { get; set; }
        public string @object { get; set; }
        public object address_city { get; set; }
        public object address_country { get; set; }
        public object address_line1 { get; set; }
        public object address_line1_check { get; set; }
        public object address_line2 { get; set; }
        public object address_state { get; set; }
        public object address_zip { get; set; }
        public object address_zip_check { get; set; }
        public string brand { get; set; }
        public string country { get; set; }
        public string cvc_check { get; set; }
        public object dynamic_last4 { get; set; }
        public int exp_month { get; set; }
        public int exp_year { get; set; }
        public string fingerprint { get; set; }
        public string funding { get; set; }
        public string last4 { get; set; }
        public object name { get; set; }
        public object tokenization_method { get; set; }
    }

    public class StripeCardResult
    {
        public string id { get; set; }
        public string @object { get; set; }
        public StripeCard card { get; set; }
        public string client_ip { get; set; }
        public int created { get; set; }
        public bool livemode { get; set; }
        public string type { get; set; }
        public bool used { get; set; }
    }
}
