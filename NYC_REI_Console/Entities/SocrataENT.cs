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
        public string ecb_issue_date { get; set; }
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
        public string job__ { get; set; }
        public string permittee_s_first_name { get; set; }
        public string permittee_s_last_name { get; set; }
        public string permittee_s_business_name { get; set; }
        public string permittee_s_license_type { get; set; }
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
    class SocrataEcbViolations
    {
        public string ISN_DOB_BIS_EXTRACT { get; set; }
        public string ECB_VIOLATION_NUMBER { get; set; }
        public string ECB_VIOLATION_STATUS { get; set; }
        public string DOB_VIOLATION_NUMBER { get; set; }
        public string BIN { get; set; }
        public string BORO { get; set; }
        public string BLOCK { get; set; }
        public string LOT { get; set; }
        public string HEARING_DATE { get; set; }
        public string HEARING_TIME { get; set; }
        public string SERVED_DATE { get; set; }
        public string ISSUE_DATE { get; set; }
        public string SEVERITY { get; set; }
        public string VIOLATION_TYPE { get; set; }
        public string RESPONDENT_NAME { get; set; }
        public string RESPONDENT_HOUSE_NUMBER { get; set; }
        public string RESPONDENT_STREET { get; set; }
        public string RESPONDENT_CITY { get; set; }
        public string RESPONDENT_ZIP { get; set; }
        public string VIOLATION_DESCRIPTION { get; set; }
        public string PENALITY_IMPOSED { get; set; }
        public string AMOUNT_PAID { get; set; }
        public string BALANCE_DUE { get; set; }
        public string AGGRAVATED_LEVEL { get; set; }
        public string HEARING_STATUS { get; set; }
        public string CERTIFICATION_STATUS { get; set; }
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
        public string Tract2010 { get; set; }
        public string LandUse { get; set; }
    }

    class JsonMapDesignationsData
    {
        public List<TableFieldsMapDesignations> features { get; set; }
    }
    class TableFieldsMapDesignations
    {
        public MapDesignationsAttributes attributes { get; set; }
    }
    class MapDesignationsAttributes
    {
        public int OBJECTID { get; set; }
        public string ENUMBER { get; set; }
        public string CEQR_NUM { get; set; }
        public string ULURP_NUM { get; set; }
        public int? BOROCODE { get; set; }
        public int? TAXBLOCK { get; set; }
        public int? TAXLOT { get; set; }
        public string ZONING_MAP { get; set; }
        public string DESCRIPTION { get; set; }
        public string BBL { get; set; }
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


    class JsonConsumerProfiles
    {
        public string state { get; set; }
        public string county { get; set; }
        public string tract { get; set; }
        public int Year { get; set; }
        public string DP05_0001E { get; set; }
        public string DP05_0002E { get; set; }
        public string DP05_0002PE { get; set; }
        public string DP05_0003E { get; set; }
        public string DP05_0003PE { get; set; }
        public string DP05_0004E { get; set; }
        public string DP05_0009E { get; set; }
        public string DP05_0010E { get; set; }
        public string DP05_0011E { get; set; }
        public string DP05_0012E { get; set; }
        public string DP05_0013E { get; set; }
        public string DP05_0014E { get; set; }
        public string DP05_0015E { get; set; }
        public string DP05_0016E { get; set; }
        public string DP02_0001E { get; set; }
        public string DP02_0001PE { get; set; }
        public string DP02_0002E { get; set; }
        public string DP02_0002PE { get; set; }
        public string DP02_0003E { get; set; }
        public string DP02_0003PE { get; set; }
        public string DP02_0006E { get; set; }
        public string DP02_0006PE { get; set; }
        public string DP02_0008E { get; set; }
        public string DP02_0008PE { get; set; }
        public string DP02_0010E { get; set; }
        public string DP02_0010PE { get; set; }
        public string DP02_0011E { get; set; }
        public string DP02_0011PE { get; set; }
        public string DP02_0012E { get; set; }
        public string DP02_0012PE { get; set; }
        public string DP02_0024E { get; set; }
        public string DP02_0024PE { get; set; }
        public string DP02_0025E { get; set; }
        public string DP02_0025PE { get; set; }
        public string DP02_0026E { get; set; }
        public string DP02_0026PE { get; set; }
        public string DP02_0027E { get; set; }
        public string DP02_0027PE { get; set; }
        public string DP02_0028E { get; set; }
        public string DP02_0028PE { get; set; }
        public string DP02_0029E { get; set; }
        public string DP02_0029PE { get; set; }
        public string DP02_0030E { get; set; }
        public string DP02_0030PE { get; set; }
        public string DP02_0031E { get; set; }
        public string DP02_0031PE { get; set; }
        public string DP02_0032E { get; set; }
        public string DP02_0032PE { get; set; }
        public string DP02_0033E { get; set; }
        public string DP02_0033PE { get; set; }
        public string DP02_0034E { get; set; }
        public string DP02_0034PE { get; set; }
        public string DP02_0035E { get; set; }
        public string DP02_0035PE { get; set; }
        public string DP02_0052E { get; set; }
        public string DP02_0052PE { get; set; }
        public string DP02_0053E { get; set; }
        public string DP02_0053PE { get; set; }
        public string DP02_0054E { get; set; }
        public string DP02_0054PE { get; set; }
        public string DP02_0055E { get; set; }
        public string DP02_0055PE { get; set; }
        public string DP02_0056E { get; set; }
        public string DP02_0056PE { get; set; }
        public string DP02_0057E { get; set; }
        public string DP02_0057PE { get; set; }
        public string DP02_0058E { get; set; }
        public string DP02_0058PE { get; set; }
        public string DP02_0059E { get; set; }
        public string DP02_0059PE { get; set; }
        public string DP02_0060E { get; set; }
        public string DP02_0060PE { get; set; }
        public string DP02_0061E { get; set; }
        public string DP02_0061PE { get; set; }
        public string DP02_0062E { get; set; }
        public string DP02_0062PE { get; set; }
        public string DP02_0063E { get; set; }
        public string DP02_0063PE { get; set; }
        public string DP02_0064E { get; set; }
        public string DP02_0064PE { get; set; }
        public string DP02_0070E { get; set; }
        public string DP02_0070PE { get; set; }
        public string DP02_0071E { get; set; }
        public string DP02_0071PE { get; set; }
        public string DP02_0074E { get; set; }
        public string DP02_0074PE { get; set; }
        public string DP02_0075E { get; set; }
        public string DP02_0075PE { get; set; }
        public string DP02_0076E { get; set; }
        public string DP02_0076PE { get; set; }
        public string DP02_0078E { get; set; }
        public string DP02_0078PE { get; set; }
        public string DP02_0079E { get; set; }
        public string DP02_0079PE { get; set; }
        public string DP02_0080E { get; set; }
        public string DP02_0080PE { get; set; }
        public string DP02_0081E { get; set; }
        public string DP02_0081PE { get; set; }
        public string DP02_0082E { get; set; }
        public string DP02_0082PE { get; set; }
        public string DP02_0083E { get; set; }
        public string DP02_0083PE { get; set; }
        public string DP02_0084E { get; set; }
        public string DP02_0084PE { get; set; }
        public string DP02_0150E { get; set; }
        public string DP02_0150PE { get; set; }
        public string DP02_0151E { get; set; }
        public string DP02_0151PE { get; set; }
        public string DP02_0152E { get; set; }
        public string DP02_0152PE { get; set; }
        public string DP03_0001E { get; set; }
        public string DP03_0001PE { get; set; }
        public string DP03_0003E { get; set; }
        public string DP03_0003PE { get; set; }
        public string DP03_0004E { get; set; }
        public string DP03_0004PE { get; set; }
        public string DP03_0005E { get; set; }
        public string DP03_0005PE { get; set; }
        public string DP03_0007E { get; set; }
        public string DP03_0007PE { get; set; }
        public string DP03_0009E { get; set; }
        public string DP03_0009PE { get; set; }
        public string DP03_0010E { get; set; }
        public string DP03_0010PE { get; set; }
        public string DP03_0012E { get; set; }
        public string DP03_0012PE { get; set; }
        public string DP03_0013E { get; set; }
        public string DP03_0013PE { get; set; }
        public string DP03_0051E { get; set; }
        public string DP03_0051PE { get; set; }
        public string DP03_0052E { get; set; }
        public string DP03_0052PE { get; set; }
        public string DP03_0053E { get; set; }
        public string DP03_0053PE { get; set; }
        public string DP03_0054E { get; set; }
        public string DP03_0054PE { get; set; }
        public string DP03_0055E { get; set; }
        public string DP03_0055PE { get; set; }
        public string DP03_0056E { get; set; }
        public string DP03_0056PE { get; set; }
        public string DP03_0057E { get; set; }
        public string DP03_0057PE { get; set; }
        public string DP03_0058E { get; set; }
        public string DP03_0058PE { get; set; }
        public string DP03_0059E { get; set; }
        public string DP03_0059PE { get; set; }
        public string DP03_0060E { get; set; }
        public string DP03_0060PE { get; set; }
        public string DP03_0061E { get; set; }
        public string DP03_0061PE { get; set; }
        public string DP03_0063E { get; set; }
        public string DP03_0063PE { get; set; }
        public string DP03_0066E { get; set; }
        public string DP03_0066PE { get; set; }
        public string DP03_0068E { get; set; }
        public string DP03_0068PE { get; set; }
        public string DP03_0069E { get; set; }
        public string DP03_0069PE { get; set; }
        public string DP03_0070E { get; set; }
        public string DP03_0070PE { get; set; }
        public string DP03_0071E { get; set; }
        public string DP03_0071PE { get; set; }
        public string DP03_0072E { get; set; }
        public string DP03_0072PE { get; set; }
        public string DP03_0073E { get; set; }
        public string DP03_0073PE { get; set; }
        public string DP03_0095E { get; set; }
        public string DP03_0095PE { get; set; }
        public string DP03_0096E { get; set; }
        public string DP03_0096PE { get; set; }
        public string DP03_0097E { get; set; }
        public string DP03_0097PE { get; set; }
        public string DP03_0098E { get; set; }
        public string DP03_0098PE { get; set; }
        public string DP03_0099E { get; set; }
        public string DP03_0099PE { get; set; }
        public string DP03_0102E { get; set; }
        public string DP03_0102PE { get; set; }
        public string DP03_0103E { get; set; }
        public string DP03_0103PE { get; set; }
        public string DP03_0104E { get; set; }
        public string DP03_0104PE { get; set; }
        public string DP03_0105E { get; set; }
        public string DP03_0105PE { get; set; }
        public string DP03_0106E { get; set; }
        public string DP03_0106PE { get; set; }
        public string DP03_0107E { get; set; }
        public string DP03_0107PE { get; set; }
        public string DP03_0108E { get; set; }
        public string DP03_0108PE { get; set; }
        public string DP03_0109E { get; set; }
        public string DP03_0109PE { get; set; }
        public string DP03_0110E { get; set; }
        public string DP03_0110PE { get; set; }
        public string DP03_0111E { get; set; }
        public string DP03_0111PE { get; set; }
        public string DP03_0112E { get; set; }
        public string DP03_0112PE { get; set; }
        public string DP03_0113E { get; set; }
        public string DP03_0113PE { get; set; }
        public string DP03_0133E { get; set; }
        public string DP03_0133PE { get; set; }
        public string DP03_0134E { get; set; }
        public string DP03_0134PE { get; set; }
        public string DP03_0135E { get; set; }
        public string DP03_0135PE { get; set; }
        public string DP04_0006E { get; set; }
        public string DP04_0006PE { get; set; }
        public string DP04_0007E { get; set; }
        public string DP04_0007PE { get; set; }
        public string DP04_0008E { get; set; }
        public string DP04_0008PE { get; set; }
        public string DP04_0009E { get; set; }
        public string DP04_0009PE { get; set; }
        public string DP04_0010E { get; set; }
        public string DP04_0010PE { get; set; }
        public string DP04_0011E { get; set; }
        public string DP04_0011PE { get; set; }
        public string DP04_0012E { get; set; }
        public string DP04_0012PE { get; set; }
        public string DP04_0013E { get; set; }
        public string DP04_0013PE { get; set; }
        public string DP04_0027E { get; set; }
        public string DP04_0027PE { get; set; }
        public string DP04_0028E { get; set; }
        public string DP04_0028PE { get; set; }
        public string DP04_0029E { get; set; }
        public string DP04_0029PE { get; set; }
        public string DP04_0030E { get; set; }
        public string DP04_0030PE { get; set; }
        public string DP04_0031E { get; set; }
        public string DP04_0031PE { get; set; }
        public string DP04_0032E { get; set; }
        public string DP04_0032PE { get; set; }
        public string DP04_0033E { get; set; }
        public string DP04_0033PE { get; set; }
        public string DP04_0034E { get; set; }
        public string DP04_0034PE { get; set; }
        public string DP04_0035E { get; set; }
        public string DP04_0035PE { get; set; }
        public string DP04_0036E { get; set; }
        public string DP04_0036PE { get; set; }
        public string DP04_0037E { get; set; }
        public string DP04_0037PE { get; set; }
        public string DP04_0038E { get; set; }
        public string DP04_0038PE { get; set; }
        public string DP04_0039E { get; set; }
        public string DP04_0039PE { get; set; }
        public string DP04_0040E { get; set; }
        public string DP04_0040PE { get; set; }
        public string DP04_0041E { get; set; }
        public string DP04_0041PE { get; set; }
        public string DP04_0042E { get; set; }
        public string DP04_0042PE { get; set; }
        public string DP04_0043E { get; set; }
        public string DP04_0043PE { get; set; }
        public string DP04_0044E { get; set; }
        public string DP04_0044PE { get; set; }
        public string DP04_0045E { get; set; }
        public string DP04_0045PE { get; set; }
        public string DP04_0046E { get; set; }
        public string DP04_0046PE { get; set; }
        public string DP04_0047E { get; set; }
        public string DP04_0047PE { get; set; }
        public string DP04_0050E { get; set; }
        public string DP04_0050PE { get; set; }
        public string DP04_0051E { get; set; }
        public string DP04_0051PE { get; set; }
        public string DP04_0052E { get; set; }
        public string DP04_0052PE { get; set; }
        public string DP04_0053E { get; set; }
        public string DP04_0053PE { get; set; }
        public string DP04_0054E { get; set; }
        public string DP04_0054PE { get; set; }
        public string DP04_0055E { get; set; }
        public string DP04_0055PE { get; set; }
        public string DP04_0056E { get; set; }
        public string DP04_0056PE { get; set; }
        public string DP04_0076E { get; set; }
        public string DP04_0076PE { get; set; }
        public string DP04_0077E { get; set; }
        public string DP04_0077PE { get; set; }
        public string DP04_0078E { get; set; }
        public string DP04_0078PE { get; set; }
        public string DP04_0079E { get; set; }
        public string DP04_0079PE { get; set; }
        public string DP04_0090E { get; set; }
        public string DP04_0090PE { get; set; }
        public string DP04_0091E { get; set; }
        public string DP04_0091PE { get; set; }
        public string DP04_0092E { get; set; }
        public string DP04_0092PE { get; set; }
        public string DP04_0093E { get; set; }
        public string DP04_0093PE { get; set; }
        public string DP04_0094E { get; set; }
        public string DP04_0094PE { get; set; }
        public string DP04_0095E { get; set; }
        public string DP04_0095PE { get; set; }
        public string DP04_0096E { get; set; }
        public string DP04_0096PE { get; set; }
        public string DP04_0097E { get; set; }
        public string DP04_0097PE { get; set; }
        public string DP04_0098E { get; set; }
        public string DP04_0098PE { get; set; }
        public string DP04_0099E { get; set; }
        public string DP04_0099PE { get; set; }
        public string DP04_0100E { get; set; }
        public string DP04_0100PE { get; set; }
        public string DP04_0101E { get; set; }
        public string DP04_0101PE { get; set; }
        public string DP04_0110E { get; set; }
        public string DP04_0110PE { get; set; }
        public string DP04_0111E { get; set; }
        public string DP04_0111PE { get; set; }
        public string DP04_0112E { get; set; }
        public string DP04_0112PE { get; set; }
        public string DP04_0113E { get; set; }
        public string DP04_0113PE { get; set; }
        public string DP04_0114E { get; set; }
        public string DP04_0114PE { get; set; }
        public string DP04_0115E { get; set; }
        public string DP04_0115PE { get; set; }
        public string DP04_0126E { get; set; }
        public string DP04_0126PE { get; set; }
        public string DP04_0127E { get; set; }
        public string DP04_0127PE { get; set; }
        public string DP04_0128E { get; set; }
        public string DP04_0128PE { get; set; }
        public string DP04_0129E { get; set; }
        public string DP04_0129PE { get; set; }
        public string DP04_0130E { get; set; }
        public string DP04_0130PE { get; set; }
        public string DP04_0131E { get; set; }
        public string DP04_0131PE { get; set; }
        public string DP04_0132E { get; set; }
        public string DP04_0132PE { get; set; }
        public string DP04_0133E { get; set; }
        public string DP04_0133PE { get; set; }
        public string DP04_0134E { get; set; }
        public string DP04_0134PE { get; set; }
        public string DP04_0136E { get; set; }
        public string DP04_0136PE { get; set; }
        public string DP04_0137E { get; set; }
        public string DP04_0137PE { get; set; }
        public string DP04_0138E { get; set; }
        public string DP04_0138PE { get; set; }
        public string DP04_0139E { get; set; }
        public string DP04_0139PE { get; set; }
        public string DP04_0140E { get; set; }
        public string DP04_0140PE { get; set; }
        public string DP04_0141E { get; set; }
        public string DP04_0141PE { get; set; }
        public string DP04_0142E { get; set; }
        public string DP04_0142PE { get; set; }
    }
}
