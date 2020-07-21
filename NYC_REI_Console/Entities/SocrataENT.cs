using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYC_REI_Console.Entities
{
    class DatabaseMaxValues
    {
        public int? OBJECTID { get; set; }
        public DateTime? generation_date { get; set; }
        public DateTime? dobrundate { get; set; }
        public string issue_date { get; set; }
        public DateTime? EXECUTED_DATE { get; set; }
        public int? DistrictOBJECTID { get; set; }
        public DateTime? filing_date { get; set; }
        public DateTime? sale_date { get; set; }
    }
    class SocrataEnergy
    {
        public int order { get; set; }
        public int? property_id { get; set; }
        public string property_name { get; set; }
        public Int64? parent_property_id { get; set; }
        public string parent_property_name { get; set; }
        public string bbl_10_digits { get; set; }
        public string nyc_borough_block_and_lot { get; set; }
        public string nyc_building_identification { get; set; }
        public string address_1_self_reported { get; set; }
        public string address_2_self_reported { get; set; }
        public string postal_code { get; set; }
        public string street_number { get; set; }
        public string street_name { get; set; }
        public string borough { get; set; }
        public string dof_gross_floor_area_ft { get; set; }
        public int? self_reported_gross_floor { get; set; }
        public string primary_property_type_self { get; set; }
        public string list_of_all_property_use { get; set; }
        public string largest_property_use_type { get; set; }
        public string largest_property_use_type_1 { get; set; }
        public string _2nd_largest_property_use { get; set; }
        public string _2nd_largest_property_use_1 { get; set; }
        public string _3rd_largest_property_use { get; set; }
        public string _3rd_largest_property_use_1 { get; set; }
        public int? year_built { get; set; }
        public int? number_of_buildings { get; set; }
        public int? occupancy { get; set; }
        public string metered_areas_energy { get; set; }
        public string metered_areas_water { get; set; }
        public int? energy_star_score { get; set; }
        public float? source_eui_kbtu_ft { get; set; }
        public float? weather_normalized_source { get; set; }
        public float? site_eui_kbtu_ft { get; set; }
        public float? weather_normalized_site_eui { get; set; }
        public float? weather_normalized_site { get; set; }
        public float? weather_normalized_site_1 { get; set; }
        public string fuel_oil_1_use_kbtu { get; set; }
        public float? fuel_oil_2_use_kbtu { get; set; }
        public float? fuel_oil_4_use_kbtu { get; set; }
        public float? fuel_oil_5_6_use_kbtu { get; set; }
        public string diesel_2_use_kbtu { get; set; }
        public string propane_use_kbtu { get; set; }
        public float? district_steam_use_kbtu { get; set; }
        public string district_hot_water_use_kbtu { get; set; }
        public string district_chilled_water_use { get; set; }
        public float? natural_gas_use_kbtu { get; set; }
        public float? weather_normalized_site_2 { get; set; }
        public float? electricity_use_grid_purchase { get; set; }
        public float? electricity_use_grid_purchase_1 { get; set; }
        public float? weather_normalized_site_3 { get; set; }
        public float? annual_maximum_demand_kw { get; set; }
        public DateTime? annual_maximum_demand_mm { get; set; }
        public float? total_ghg_emissions_metric { get; set; }
        public float? direct_ghg_emissions_metric { get; set; }
        public float? indirect_ghg_emissions_metric { get; set; }
        public float? water_use_all_water_sources { get; set; }
        public float? water_use_intensity_all_water { get; set; }
        public string water_required { get; set; }
        public DateTime? generation_date { get; set; }
        public string dof_benchmarking_submission { get; set; }
        public float? latitude { get; set; }
        public float? longitude { get; set; }
        public int? community_board { get; set; }
        public int? council_district { get; set; }
        public int? census_tract { get; set; }
        public string nta { get; set; }
    }
    class SocrataPermits
    {
        public string borough { get; set; }
        public string block { get; set; }
        public string lot { get; set; }
        public DateTime? job_start_date { get; set; }
        public string job_type { get; set; }
        public string work_type { get; set; }
        public string bbl_10_digits { get; set; }
        public DateTime? dobrundate { get; set; }
    }
    class SocrataViolations
    {
        public string isn_dob_bis_viol { get; set; }
        public string boro { get; set; }
        public string bin { get; set; }
        public string block { get; set; }
        public string lot { get; set; }
        public string issue_date { get; set; }
        public string violation_type_code { get; set; }
        public string violation_number { get; set; }
        public string house_number { get; set; }
        public string street { get; set; }
        public string disposition_date { get; set; }
        public string disposition_comments { get; set; }
        public string device_number { get; set; }
        public string description { get; set; }
        public string ecb_number { get; set; }
        public string number { get; set; }
        public string violation_category { get; set; }
        public string violation_type { get; set; }
    }
    class SocrataEvictions
    {
        public string COURT_INDEX_NUMBER { get; set; }
        public string DOCKET_NUMBER { get; set; }
        public string EVICTION_ADDRESS { get; set; }
        public string EVICTION_APT_NUM { get; set; }
        public DateTime? EXECUTED_DATE { get; set; }
        public string MARSHAL_FIRST_NAME { get; set; }
        public string MARSHAL_LAST_NAME { get; set; }
        public string RESIDENTIAL_COMMERCIAL_IND { get; set; }
        public string BOROUGH { get; set; }
        public string EVICTION_ZIP { get; set; }
    }
    class SocrataElevators
    {
        public string job_filing_number { get; set; }
        public string job_number { get; set; }
        public string filing_number { get; set; }
        public DateTime? filing_date { get; set; }
        public string filing_type { get; set; }
        public string elevatordevicetype { get; set; }
        public string filing_status { get; set; }
        public string filingstatus_or_filingincludes { get; set; }
        public string building_code { get; set; }
        public string electrical_permit_number { get; set; }
        public string bin { get; set; }
        public string house_number { get; set; }
        public string street_name { get; set; }
        public string zip { get; set; }
        public string borough { get; set; }
        public string block { get; set; }
        public string lot { get; set; }
        public string building_type { get; set; }
        public string buildingstories { get; set; }
        public string bbl { get; set; }

    }
    class SocrataPropertySales
    {
        public int? borough { get; set; }
        public string neighborhood { get; set; }
        public string building_class_category { get; set; }
        public string tax_class_at_present { get; set; }
        public int? block { get; set; }
        public int? lot { get; set; }
        public string ease_ment { get; set; }
        public string building_class_at_present { get; set; }
        public string address { get; set; }
        public string apartment_number { get; set; }
        public int? zip_code { get; set; }
        public int? residential_units { get; set; }
        public int? commercial_units { get; set; }
        public int? total_units { get; set; }
        public string land_square_feet { get; set; }
        public int? gross_square_feet { get; set; }
        public int? year_built { get; set; }
        public int? tax_class_at_time_of_sale { get; set; }
        public string building_class_at_time_of { get; set; }
        public string sale_price { get; set; }
        public DateTime? sale_date { get; set; }
        public string bbl { get; set; }
    }
    class JsonMapPlutoData
    {
        public List<TableFieldsMapPluto> features { get; set; }
    }
    class TableFieldsMapPluto
    {
        public MapPlutoAttributes attributes { get; set; }
    }
    class MapPlutoAttributes
    {
        public int OBJECTID { get; set; }
        public string BBL { get; set; }
        public string Borough { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? BldgArea { get; set; }
        public int? ComArea { get; set; }
        public int? ResArea { get; set; }
        public float? NumFloors { get; set; }
        public string UnitsRes { get; set; }
        public string ZoneDist1 { get; set; }
        public string Overlay1 { get; set; }
        public string Overlay2 { get; set; }
        public float? AssessTot { get; set; }
        public int? YearBuilt { get; set; }
        public string OwnerName { get; set; }
        public string BldgClass { get; set; }
        public int? CD { get; set; }
    }
    public class DatabaseAttributes
    {
        public int OBJECTID { get; set; }
        public string Borough { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? BldgArea { get; set; }
        public int? ComArea { get; set; }
        public int? ResArea { get; set; }
        public double? NumFloors { get; set; }
        public string UnitsRes { get; set; }
        public string ZoneDist1 { get; set; }
        public string Overlay1 { get; set; }
        public string Overlay2 { get; set; }
        public double? AssessTot { get; set; }
        public int? YearBuilt { get; set; }
        public string BldgClass { get; set; }
        public string OwnerName { get; set; }

        public int? energy_star_score { get; set; }
        public double? source_eui_kbtu_ft { get; set; }
        public double? site_eui_kbtu_ft { get; set; }
        public double? annual_maximum_demand_kw { get; set; }
        public double? total_ghg_emissions_metric { get; set; }

        private DateTime? _job_start_date;
        public DateTime? job_start_date
        {
            get { return _job_start_date; }
            set
            {
                _job_start_date = value;

                job_start_date_string_format = _job_start_date.HasValue ? String.Format("{0:MM/dd/yyyy}", _job_start_date) : "";
            }
        }
        public string job_start_date_string_format { get; set; }
        public string job_type { get; set; }
        public string work_type { get; set; }

        private string _issue_date;
        public string issue_date
        {
            get { return _issue_date; }
            set
            {
                _issue_date = value;

                issue_date_string_format = _issue_date.Length == 8 ? _issue_date.Substring(4, 2) + "/" + _issue_date.Substring(6, 2) + "/" + _issue_date.Substring(0, 4) : "";
            }
        }
        public string issue_date_string_format { get; set; }
        public string violation_type { get; set; }
        public string violation_category { get; set; }

        private DateTime? _executed_date;
        public DateTime? executed_date
        {
            get { return _executed_date; }
            set
            {
                _executed_date = value;

                executed_date_string_format = _executed_date.HasValue ? String.Format("{0:MM/dd/yyyy}", _executed_date) : "";
            }
        }
        public string executed_date_string_format { get; set; }
    }
    public static class Properties
    {
        public const string EmailSender = "nyc.rei.dev@gmail.com";
        public const string Password = "Delagarza1!!";
        public const string SMTPClient = "smtp.gmail.com";
        public const string SMTPPort = "587";
    }
    class JsonDistrictData
    {
        public List<TableFieldsDistrict> features { get; set; }
    }
    class TableFieldsDistrict
    {
        public DistrictAttributes attributes { get; set; }
    }
    class DistrictAttributes
    {
        public int OBJECTID { get; set; }
        public int DISTRICTCODE { get; set; }
        public string DISTRICT { get; set; }
    }
}
