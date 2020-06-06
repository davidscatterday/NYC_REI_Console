using NYC_REI_Console.Entities;
using NYC_REI_Console.Helpers;
using NYC_REI_Console.Models;
using SODA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NYC_REI_Console.DataAccessLayer
{
    class SocrataDataDAL
    {
        private NYC_Web_Mapping_AppEntities db = new NYC_Web_Mapping_AppEntities();

        public void GetAllMapPluto(int? OBJECTID)
        {
            int minObjectID = OBJECTID.HasValue ? OBJECTID.Value + 1 : 0;
            JsonMapPlutoData data = new JsonMapPlutoData();
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            int step = 100000;
            int ObjectIDmin = minObjectID + 1;
            int ObjectIDmax = step + minObjectID + 1;
            while (true)
            {
                using (var client = new WebClient())
                {
                    string whereClaues = "objectid >= " + ObjectIDmin + " AND objectid < " + ObjectIDmax;
                    ObjectIDmin = ObjectIDmax;
                    ObjectIDmax += step;
                    string urlPost = "https://services5.arcgis.com/GfwWNkhOj9bNBqoJ/arcgis/rest/services/MAPPLUTO/FeatureServer/0/query?where=" + whereClaues + "&f=pjson&returnGeometry=false&outFields=OBJECTID,BBL,Borough,Address,ZipCode,Latitude,Longitude,BldgArea,ComArea,ResArea,NumFloors,UnitsRes,ZoneDist1,Overlay1,Overlay2,AssessTot,YearBuilt,OwnerName,BldgClass,CD";
                    var json = client.DownloadString(urlPost);

                    data = serializer.Deserialize<JsonMapPlutoData>(json);

                    if (data.features.Count() == 0)
                    {
                        break;
                    }
                    else
                    {
                        foreach (TableFieldsMapPluto tf in data.features)
                        {
                            using (var ctx = new NYC_Web_Mapping_AppEntities())
                            {
                                var OBJECTIDParametar = new SqlParameter("OBJECTID", tf.attributes.OBJECTID);
                                var BBLParametar = !String.IsNullOrEmpty(tf.attributes.BBL) ? new SqlParameter("BBL", tf.attributes.BBL) : new SqlParameter("BBL", DBNull.Value);
                                var BoroughParametar = !String.IsNullOrEmpty(tf.attributes.Borough) ? new SqlParameter("Borough", tf.attributes.Borough) : new SqlParameter("Borough", DBNull.Value);
                                var AddressParametar = !String.IsNullOrEmpty(tf.attributes.Address) ? new SqlParameter("Address", tf.attributes.Address) : new SqlParameter("Address", DBNull.Value);
                                var ZipCodeParametar = !String.IsNullOrEmpty(tf.attributes.ZipCode) ? new SqlParameter("ZipCode", tf.attributes.ZipCode) : new SqlParameter("ZipCode", DBNull.Value);
                                var LatitudeParametar = !String.IsNullOrEmpty(tf.attributes.Latitude) ? new SqlParameter("Latitude", tf.attributes.Latitude) : new SqlParameter("Latitude", DBNull.Value);
                                var LongitudeParametar = !String.IsNullOrEmpty(tf.attributes.Longitude) ? new SqlParameter("Longitude", tf.attributes.Longitude) : new SqlParameter("Longitude", DBNull.Value);
                                var BldgAreaParametar = tf.attributes.BldgArea.HasValue ? new SqlParameter("BldgArea", tf.attributes.BldgArea) : new SqlParameter("BldgArea", DBNull.Value);
                                var ComAreaParametar = tf.attributes.ComArea.HasValue ? new SqlParameter("ComArea", tf.attributes.ComArea) : new SqlParameter("ComArea", DBNull.Value);
                                var ResAreaParametar = tf.attributes.ResArea.HasValue ? new SqlParameter("ResArea", tf.attributes.ResArea) : new SqlParameter("ResArea", DBNull.Value);
                                var NumFloorsParametar = tf.attributes.NumFloors.HasValue ? new SqlParameter("NumFloors", tf.attributes.NumFloors) : new SqlParameter("NumFloors", DBNull.Value);
                                var UnitsResParametar = !String.IsNullOrEmpty(tf.attributes.UnitsRes) ? new SqlParameter("UnitsRes", tf.attributes.UnitsRes) : new SqlParameter("UnitsRes", DBNull.Value);
                                var ZoneDist1Parametar = !String.IsNullOrEmpty(tf.attributes.ZoneDist1) ? new SqlParameter("ZoneDist1", tf.attributes.ZoneDist1) : new SqlParameter("ZoneDist1", DBNull.Value);
                                var Overlay1Parametar = !String.IsNullOrEmpty(tf.attributes.Overlay1) ? new SqlParameter("Overlay1", tf.attributes.Overlay1) : new SqlParameter("Overlay1", DBNull.Value);
                                var Overlay2Parametar = !String.IsNullOrEmpty(tf.attributes.Overlay2) ? new SqlParameter("Overlay2", tf.attributes.Overlay2) : new SqlParameter("Overlay2", DBNull.Value);
                                var AssessTotParametar = tf.attributes.AssessTot.HasValue ? new SqlParameter("AssessTot", tf.attributes.AssessTot) : new SqlParameter("AssessTot", DBNull.Value);
                                var YearBuiltParametar = tf.attributes.YearBuilt.HasValue ? new SqlParameter("YearBuilt", tf.attributes.YearBuilt) : new SqlParameter("YearBuilt", DBNull.Value);
                                var OwnerNameParametar = !String.IsNullOrEmpty(tf.attributes.OwnerName) ? new SqlParameter("OwnerName", tf.attributes.OwnerName) : new SqlParameter("OwnerName", DBNull.Value);
                                var BldgClassParametar = !String.IsNullOrEmpty(tf.attributes.BldgClass) ? new SqlParameter("BldgClass", tf.attributes.BldgClass) : new SqlParameter("BldgClass", DBNull.Value);
                                var CDParametar = tf.attributes.CD.HasValue ? new SqlParameter("CD", tf.attributes.CD) : new SqlParameter("CD", DBNull.Value);

                                //ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertMapPluto @OBJECTID, @BBL, @Borough, @Address, @ZipCode, @Latitude, @Longitude, @BldgArea, @ComArea, @ResArea, @NumFloors, @UnitsRes, @ZoneDist1, @Overlay1, @Overlay2, @AssessTot, @YearBuilt, @OwnerName, @BldgClass, @CD ",
                                //    OBJECTIDParametar, BBLParametar, BoroughParametar, AddressParametar, ZipCodeParametar, LatitudeParametar, LongitudeParametar
                                //    , BldgAreaParametar, ComAreaParametar, ResAreaParametar, NumFloorsParametar, UnitsResParametar, ZoneDist1Parametar, Overlay1Parametar
                                //    , Overlay2Parametar, AssessTotParametar, YearBuiltParametar, OwnerNameParametar, BldgClassParametar, CDParametar);

                                ctx.Database.ExecuteSqlCommand("EXEC dbo.UpdateMapPluto @OBJECTID, @CD ",
                                    OBJECTIDParametar, CDParametar);
                            }
                        }
                    }
                }
            }
        }
        public void InsertAllEnergy(DateTime? generation_date)
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<SocrataEnergy>(GlobalVariables.EnergyID);

            IEnumerable<SocrataEnergy> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                if (generation_date.HasValue)
                {
                    var strDate = generation_date.Value.Year + "-" + generation_date.Value.Month + "-" + generation_date.Value.Day;
                    var soql = new SoqlQuery()
                          .Where("generation_date > '" + strDate + "'")
                          .Limit(myLimit)
                          .Offset(myOffset);
                    rows = dataset.Query<SocrataEnergy>(soql);
                }
                else
                {
                    rows = dataset.GetRows(limit: myLimit, offset: myOffset);
                }
                if (rows.Count() == 0)
                {
                    break;
                }
                else
                {
                    foreach (var keyValue in rows)
                    {
                        using (var ctx = new NYC_Web_Mapping_AppEntities())
                        {
                            var orderParametar = new SqlParameter("order", keyValue.order);
                            var property_idParametar = keyValue.property_id.HasValue ? new SqlParameter("property_id", keyValue.property_id) : new SqlParameter("property_id", DBNull.Value);
                            var property_nameParametar = !String.IsNullOrEmpty(keyValue.property_name) ? new SqlParameter("property_name", keyValue.property_name) : new SqlParameter("property_name", DBNull.Value);
                            var parent_property_idParametar = keyValue.parent_property_id.HasValue ? new SqlParameter("parent_property_id", keyValue.parent_property_id) : new SqlParameter("parent_property_id", DBNull.Value);
                            var parent_property_nameParametar = !String.IsNullOrEmpty(keyValue.parent_property_name) ? new SqlParameter("parent_property_name", keyValue.parent_property_name) : new SqlParameter("parent_property_name", DBNull.Value);
                            var bbl_10_digitsParametar = !String.IsNullOrEmpty(keyValue.bbl_10_digits) ? new SqlParameter("bbl_10_digits", keyValue.bbl_10_digits) : new SqlParameter("bbl_10_digits", DBNull.Value);
                            var nyc_borough_block_and_lotParametar = !String.IsNullOrEmpty(keyValue.nyc_borough_block_and_lot) ? new SqlParameter("nyc_borough_block_and_lot", keyValue.nyc_borough_block_and_lot) : new SqlParameter("nyc_borough_block_and_lot", DBNull.Value);
                            var nyc_building_identificationParametar = !String.IsNullOrEmpty(keyValue.nyc_building_identification) ? new SqlParameter("nyc_building_identification", keyValue.nyc_building_identification) : new SqlParameter("nyc_building_identification", DBNull.Value);
                            var address_1_self_reportedParametar = !String.IsNullOrEmpty(keyValue.address_1_self_reported) ? new SqlParameter("address_1_self_reported", keyValue.address_1_self_reported) : new SqlParameter("address_1_self_reported", DBNull.Value);
                            var address_2_self_reportedParametar = !String.IsNullOrEmpty(keyValue.address_2_self_reported) ? new SqlParameter("address_2_self_reported", keyValue.address_2_self_reported) : new SqlParameter("address_2_self_reported", DBNull.Value);
                            var postal_codeParametar = !String.IsNullOrEmpty(keyValue.postal_code) ? new SqlParameter("postal_code", keyValue.postal_code) : new SqlParameter("postal_code", DBNull.Value);
                            var street_numberParametar = !String.IsNullOrEmpty(keyValue.street_number) ? new SqlParameter("street_number", keyValue.street_number) : new SqlParameter("street_number", DBNull.Value);
                            var street_nameParametar = !String.IsNullOrEmpty(keyValue.street_name) ? new SqlParameter("street_name", keyValue.street_name) : new SqlParameter("street_name", DBNull.Value);
                            var boroughParametar = !String.IsNullOrEmpty(keyValue.borough) ? new SqlParameter("borough", keyValue.borough) : new SqlParameter("borough", DBNull.Value);
                            var dof_gross_floor_area_ftParametar = !String.IsNullOrEmpty(keyValue.dof_gross_floor_area_ft) ? new SqlParameter("dof_gross_floor_area_ft", keyValue.dof_gross_floor_area_ft) : new SqlParameter("dof_gross_floor_area_ft", DBNull.Value);
                            var self_reported_gross_floorParametar = keyValue.self_reported_gross_floor.HasValue ? new SqlParameter("self_reported_gross_floor", keyValue.self_reported_gross_floor) : new SqlParameter("self_reported_gross_floor", DBNull.Value);
                            var primary_property_type_selfParametar = !String.IsNullOrEmpty(keyValue.primary_property_type_self) ? new SqlParameter("primary_property_type_self", keyValue.primary_property_type_self) : new SqlParameter("primary_property_type_self", DBNull.Value);
                            var list_of_all_property_useParametar = !String.IsNullOrEmpty(keyValue.list_of_all_property_use) ? new SqlParameter("list_of_all_property_use", keyValue.list_of_all_property_use) : new SqlParameter("list_of_all_property_use", DBNull.Value);
                            var largest_property_use_typeParametar = !String.IsNullOrEmpty(keyValue.largest_property_use_type) ? new SqlParameter("largest_property_use_type", keyValue.largest_property_use_type) : new SqlParameter("largest_property_use_type", DBNull.Value);
                            var largest_property_use_type_1Parametar = !String.IsNullOrEmpty(keyValue.largest_property_use_type_1) ? new SqlParameter("largest_property_use_type_1", keyValue.largest_property_use_type_1) : new SqlParameter("largest_property_use_type_1", DBNull.Value);
                            var _2nd_largest_property_useParametar = !String.IsNullOrEmpty(keyValue._2nd_largest_property_use) ? new SqlParameter("_2nd_largest_property_use", keyValue._2nd_largest_property_use) : new SqlParameter("_2nd_largest_property_use", DBNull.Value);
                            var _2nd_largest_property_use_1Parametar = !String.IsNullOrEmpty(keyValue._2nd_largest_property_use_1) ? new SqlParameter("_2nd_largest_property_use_1", keyValue._2nd_largest_property_use_1) : new SqlParameter("_2nd_largest_property_use_1", DBNull.Value);
                            var _3rd_largest_property_useParametar = !String.IsNullOrEmpty(keyValue._3rd_largest_property_use) ? new SqlParameter("_3rd_largest_property_use", keyValue._3rd_largest_property_use) : new SqlParameter("_3rd_largest_property_use", DBNull.Value);
                            var _3rd_largest_property_use_1Parametar = !String.IsNullOrEmpty(keyValue._3rd_largest_property_use_1) ? new SqlParameter("_3rd_largest_property_use_1", keyValue._3rd_largest_property_use_1) : new SqlParameter("_3rd_largest_property_use_1", DBNull.Value);
                            var year_builtParametar = keyValue.year_built.HasValue ? new SqlParameter("year_built", keyValue.year_built) : new SqlParameter("year_built", DBNull.Value);
                            var number_of_buildingsParametar = keyValue.number_of_buildings.HasValue ? new SqlParameter("number_of_buildings", keyValue.number_of_buildings) : new SqlParameter("number_of_buildings", DBNull.Value);
                            var occupancyParametar = keyValue.occupancy.HasValue ? new SqlParameter("occupancy", keyValue.occupancy) : new SqlParameter("occupancy", DBNull.Value);
                            var metered_areas_energyParametar = !String.IsNullOrEmpty(keyValue.metered_areas_energy) ? new SqlParameter("metered_areas_energy", keyValue.metered_areas_energy) : new SqlParameter("metered_areas_energy", DBNull.Value);
                            var metered_areas_waterParametar = !String.IsNullOrEmpty(keyValue.metered_areas_water) ? new SqlParameter("metered_areas_water", keyValue.metered_areas_water) : new SqlParameter("metered_areas_water", DBNull.Value);
                            var energy_star_scoreParametar = keyValue.energy_star_score.HasValue ? new SqlParameter("energy_star_score", keyValue.energy_star_score) : new SqlParameter("energy_star_score", DBNull.Value);
                            var source_eui_kbtu_ftParametar = keyValue.source_eui_kbtu_ft.HasValue ? new SqlParameter("source_eui_kbtu_ft", keyValue.source_eui_kbtu_ft) : new SqlParameter("source_eui_kbtu_ft", DBNull.Value);
                            var weather_normalized_sourceParametar = keyValue.weather_normalized_source.HasValue ? new SqlParameter("weather_normalized_source", keyValue.weather_normalized_source) : new SqlParameter("weather_normalized_source", DBNull.Value);
                            var site_eui_kbtu_ftParametar = keyValue.site_eui_kbtu_ft.HasValue ? new SqlParameter("site_eui_kbtu_ft", keyValue.site_eui_kbtu_ft) : new SqlParameter("site_eui_kbtu_ft", DBNull.Value);
                            var weather_normalized_site_euiParametar = keyValue.weather_normalized_site_eui.HasValue ? new SqlParameter("weather_normalized_site_eui", keyValue.weather_normalized_site_eui) : new SqlParameter("weather_normalized_site_eui", DBNull.Value);
                            var weather_normalized_siteParametar = keyValue.weather_normalized_site.HasValue ? new SqlParameter("weather_normalized_site", keyValue.weather_normalized_site) : new SqlParameter("weather_normalized_site", DBNull.Value);
                            var weather_normalized_site_1Parametar = keyValue.weather_normalized_site_1.HasValue ? new SqlParameter("weather_normalized_site_1", keyValue.weather_normalized_site_1) : new SqlParameter("weather_normalized_site_1", DBNull.Value);
                            var fuel_oil_1_use_kbtuParametar = !String.IsNullOrEmpty(keyValue.fuel_oil_1_use_kbtu) ? new SqlParameter("fuel_oil_1_use_kbtu", keyValue.fuel_oil_1_use_kbtu) : new SqlParameter("fuel_oil_1_use_kbtu", DBNull.Value);
                            var fuel_oil_2_use_kbtuParametar = keyValue.fuel_oil_2_use_kbtu.HasValue ? new SqlParameter("fuel_oil_2_use_kbtu", keyValue.fuel_oil_2_use_kbtu) : new SqlParameter("fuel_oil_2_use_kbtu", DBNull.Value);
                            var fuel_oil_4_use_kbtuParametar = keyValue.fuel_oil_4_use_kbtu.HasValue ? new SqlParameter("fuel_oil_4_use_kbtu", keyValue.fuel_oil_4_use_kbtu) : new SqlParameter("fuel_oil_4_use_kbtu", DBNull.Value);
                            var fuel_oil_5_6_use_kbtuParametar = keyValue.fuel_oil_5_6_use_kbtu.HasValue ? new SqlParameter("fuel_oil_5_6_use_kbtu", keyValue.fuel_oil_5_6_use_kbtu) : new SqlParameter("fuel_oil_5_6_use_kbtu", DBNull.Value);
                            var diesel_2_use_kbtuParametar = !String.IsNullOrEmpty(keyValue.diesel_2_use_kbtu) ? new SqlParameter("diesel_2_use_kbtu", keyValue.diesel_2_use_kbtu) : new SqlParameter("diesel_2_use_kbtu", DBNull.Value);
                            var propane_use_kbtuParametar = !String.IsNullOrEmpty(keyValue.propane_use_kbtu) ? new SqlParameter("propane_use_kbtu", keyValue.propane_use_kbtu) : new SqlParameter("propane_use_kbtu", DBNull.Value);
                            var district_steam_use_kbtuParametar = keyValue.district_steam_use_kbtu.HasValue ? new SqlParameter("district_steam_use_kbtu", keyValue.district_steam_use_kbtu) : new SqlParameter("district_steam_use_kbtu", DBNull.Value);
                            var district_hot_water_use_kbtuParametar = !String.IsNullOrEmpty(keyValue.district_hot_water_use_kbtu) ? new SqlParameter("district_hot_water_use_kbtu", keyValue.district_hot_water_use_kbtu) : new SqlParameter("district_hot_water_use_kbtu", DBNull.Value);
                            var district_chilled_water_useParametar = !String.IsNullOrEmpty(keyValue.district_chilled_water_use) ? new SqlParameter("district_chilled_water_use", keyValue.district_chilled_water_use) : new SqlParameter("district_chilled_water_use", DBNull.Value);
                            var natural_gas_use_kbtuParametar = keyValue.natural_gas_use_kbtu.HasValue ? new SqlParameter("natural_gas_use_kbtu", keyValue.natural_gas_use_kbtu) : new SqlParameter("natural_gas_use_kbtu", DBNull.Value);
                            var weather_normalized_site_2Parametar = keyValue.weather_normalized_site_2.HasValue ? new SqlParameter("weather_normalized_site_2", keyValue.weather_normalized_site_2) : new SqlParameter("weather_normalized_site_2", DBNull.Value);
                            var electricity_use_grid_purchaseParametar = keyValue.electricity_use_grid_purchase.HasValue ? new SqlParameter("electricity_use_grid_purchase", keyValue.electricity_use_grid_purchase) : new SqlParameter("electricity_use_grid_purchase", DBNull.Value);
                            var electricity_use_grid_purchase_1Parametar = keyValue.electricity_use_grid_purchase_1.HasValue ? new SqlParameter("electricity_use_grid_purchase_1", keyValue.electricity_use_grid_purchase_1) : new SqlParameter("electricity_use_grid_purchase_1", DBNull.Value);
                            var weather_normalized_site_3Parametar = keyValue.weather_normalized_site_3.HasValue ? new SqlParameter("weather_normalized_site_3", keyValue.weather_normalized_site_3) : new SqlParameter("weather_normalized_site_3", DBNull.Value);
                            var annual_maximum_demand_kwParametar = keyValue.annual_maximum_demand_kw.HasValue ? new SqlParameter("annual_maximum_demand_kw", keyValue.annual_maximum_demand_kw) : new SqlParameter("annual_maximum_demand_kw", DBNull.Value);
                            var annual_maximum_demand_mmParametar = keyValue.annual_maximum_demand_mm.HasValue ? new SqlParameter("annual_maximum_demand_mm", keyValue.annual_maximum_demand_mm) : new SqlParameter("annual_maximum_demand_mm", DBNull.Value);
                            var total_ghg_emissions_metricParametar = keyValue.total_ghg_emissions_metric.HasValue ? new SqlParameter("total_ghg_emissions_metric", keyValue.total_ghg_emissions_metric) : new SqlParameter("total_ghg_emissions_metric", DBNull.Value);
                            var direct_ghg_emissions_metricParametar = keyValue.direct_ghg_emissions_metric.HasValue ? new SqlParameter("direct_ghg_emissions_metric", keyValue.direct_ghg_emissions_metric) : new SqlParameter("direct_ghg_emissions_metric", DBNull.Value);
                            var indirect_ghg_emissions_metricParametar = keyValue.indirect_ghg_emissions_metric.HasValue ? new SqlParameter("indirect_ghg_emissions_metric", keyValue.indirect_ghg_emissions_metric) : new SqlParameter("indirect_ghg_emissions_metric", DBNull.Value);
                            var water_use_all_water_sourcesParametar = keyValue.water_use_all_water_sources.HasValue ? new SqlParameter("water_use_all_water_sources", keyValue.water_use_all_water_sources) : new SqlParameter("water_use_all_water_sources", DBNull.Value);
                            var water_use_intensity_all_waterParametar = keyValue.water_use_intensity_all_water.HasValue ? new SqlParameter("water_use_intensity_all_water", keyValue.water_use_intensity_all_water) : new SqlParameter("water_use_intensity_all_water", DBNull.Value);
                            var water_requiredParametar = !String.IsNullOrEmpty(keyValue.water_required) ? new SqlParameter("water_required", keyValue.water_required) : new SqlParameter("water_required", DBNull.Value);
                            var generation_dateParametar = keyValue.generation_date.HasValue ? new SqlParameter("generation_date", keyValue.generation_date) : new SqlParameter("generation_date", DBNull.Value);
                            var dof_benchmarking_submissionParametar = !String.IsNullOrEmpty(keyValue.dof_benchmarking_submission) ? new SqlParameter("dof_benchmarking_submission", keyValue.dof_benchmarking_submission) : new SqlParameter("dof_benchmarking_submission", DBNull.Value);
                            var latitudeParametar = keyValue.latitude.HasValue ? new SqlParameter("latitude", keyValue.latitude) : new SqlParameter("latitude", DBNull.Value);
                            var longitudeParametar = keyValue.longitude.HasValue ? new SqlParameter("longitude", keyValue.longitude) : new SqlParameter("longitude", DBNull.Value);
                            var community_boardParametar = keyValue.community_board.HasValue ? new SqlParameter("community_board", keyValue.community_board) : new SqlParameter("community_board", DBNull.Value);
                            var council_districtParametar = keyValue.council_district.HasValue ? new SqlParameter("council_district", keyValue.council_district) : new SqlParameter("council_district", DBNull.Value);
                            var census_tractParametar = keyValue.census_tract.HasValue ? new SqlParameter("census_tract", keyValue.census_tract) : new SqlParameter("census_tract", DBNull.Value);
                            var ntaParametar = !String.IsNullOrEmpty(keyValue.nta) ? new SqlParameter("nta", keyValue.nta) : new SqlParameter("nta", DBNull.Value);

                            string executeCommand = "EXEC dbo.InsertEnergy @order, @property_id, @property_name, @parent_property_id, @parent_property_name, @bbl_10_digits, @nyc_borough_block_and_lot, @nyc_building_identification, @address_1_self_reported, @address_2_self_reported "
                                + ", @postal_code, @street_number, @street_name, @borough, @dof_gross_floor_area_ft, @self_reported_gross_floor, @primary_property_type_self, @list_of_all_property_use, @largest_property_use_type, @largest_property_use_type_1 "
                                + ", @_2nd_largest_property_use, @_2nd_largest_property_use_1, @_3rd_largest_property_use, @_3rd_largest_property_use_1, @year_built, @number_of_buildings, @occupancy, @metered_areas_energy, @metered_areas_water, @energy_star_score "
                                + ", @source_eui_kbtu_ft, @weather_normalized_source, @site_eui_kbtu_ft, @weather_normalized_site_eui, @weather_normalized_site, @weather_normalized_site_1, @fuel_oil_1_use_kbtu, @fuel_oil_2_use_kbtu, @fuel_oil_4_use_kbtu, @fuel_oil_5_6_use_kbtu "
                                + ", @diesel_2_use_kbtu, @propane_use_kbtu, @district_steam_use_kbtu, @district_hot_water_use_kbtu, @district_chilled_water_use, @natural_gas_use_kbtu, @weather_normalized_site_2, @electricity_use_grid_purchase, @electricity_use_grid_purchase_1, @weather_normalized_site_3 "
                                + ", @annual_maximum_demand_kw, @annual_maximum_demand_mm, @total_ghg_emissions_metric, @direct_ghg_emissions_metric, @indirect_ghg_emissions_metric, @water_use_all_water_sources, @water_use_intensity_all_water, @water_required, @generation_date, @dof_benchmarking_submission "
                                + ", @latitude, @longitude, @community_board, @council_district, @census_tract, @nta ";
                            ctx.Database.ExecuteSqlCommand(executeCommand,
                                orderParametar, property_idParametar, property_nameParametar, parent_property_idParametar, parent_property_nameParametar,
                                bbl_10_digitsParametar, nyc_borough_block_and_lotParametar, nyc_building_identificationParametar, address_1_self_reportedParametar, address_2_self_reportedParametar
                                , postal_codeParametar, street_numberParametar, street_nameParametar, boroughParametar, dof_gross_floor_area_ftParametar
                                , self_reported_gross_floorParametar, primary_property_type_selfParametar, list_of_all_property_useParametar, largest_property_use_typeParametar, largest_property_use_type_1Parametar
                                , _2nd_largest_property_useParametar, _2nd_largest_property_use_1Parametar, _3rd_largest_property_useParametar, _3rd_largest_property_use_1Parametar, year_builtParametar
                                , number_of_buildingsParametar, occupancyParametar, metered_areas_energyParametar, metered_areas_waterParametar, energy_star_scoreParametar
                                , source_eui_kbtu_ftParametar, weather_normalized_sourceParametar, site_eui_kbtu_ftParametar, weather_normalized_site_euiParametar, weather_normalized_siteParametar
                                , weather_normalized_site_1Parametar, fuel_oil_1_use_kbtuParametar, fuel_oil_2_use_kbtuParametar, fuel_oil_4_use_kbtuParametar, fuel_oil_5_6_use_kbtuParametar
                                , diesel_2_use_kbtuParametar, propane_use_kbtuParametar, district_steam_use_kbtuParametar, district_hot_water_use_kbtuParametar, district_chilled_water_useParametar
                                , natural_gas_use_kbtuParametar, weather_normalized_site_2Parametar, electricity_use_grid_purchaseParametar, electricity_use_grid_purchase_1Parametar, weather_normalized_site_3Parametar
                                , annual_maximum_demand_kwParametar, annual_maximum_demand_mmParametar, total_ghg_emissions_metricParametar, direct_ghg_emissions_metricParametar, indirect_ghg_emissions_metricParametar
                                , water_use_all_water_sourcesParametar, water_use_intensity_all_waterParametar, water_requiredParametar, generation_dateParametar, dof_benchmarking_submissionParametar
                                , latitudeParametar, longitudeParametar, community_boardParametar, council_districtParametar, census_tractParametar
                                , ntaParametar);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
        public void InsertAllPermits(DateTime? dobrundate)
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<SocrataPermits>(GlobalVariables.PermitID);

            IEnumerable<SocrataPermits> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                if (dobrundate.HasValue)
                {
                    var strDate = dobrundate.Value.Year + "-" + dobrundate.Value.Month + "-" + dobrundate.Value.Day;
                    var soql = new SoqlQuery()
                          .Where("dobrundate > '" + strDate + "'")
                          .Limit(myLimit)
                          .Offset(myOffset);
                    rows = dataset.Query<SocrataPermits>(soql);
                }
                else
                {
                    rows = dataset.GetRows(limit: myLimit, offset: myOffset);
                }
                if (rows.Count() == 0)
                {
                    break;
                }
                else
                {
                    foreach (var keyValue in rows)
                    {
                        var boroNum = "";
                        switch (keyValue.borough)
                        {
                            case "MANHATTAN":
                                boroNum = "1"; break;
                            case "BRONX":
                                boroNum = "2"; break;
                            case "BROOKLYN":
                                boroNum = "3"; break;
                            case "QUEENS":
                                boroNum = "4"; break;
                            case "STATEN ISLAND":
                                boroNum = "5"; break;
                            default:
                                return;
                        }
                        string bbl_10_digits = "";
                        if (!String.IsNullOrEmpty(boroNum) && !String.IsNullOrEmpty(keyValue.block) && !String.IsNullOrEmpty(keyValue.lot))
                        {
                            bbl_10_digits = boroNum + keyValue.block + keyValue.lot.Substring(1);
                        }
                        using (var ctx = new NYC_Web_Mapping_AppEntities())
                        {
                            bool validDate = false;
                            if (keyValue.job_start_date.HasValue && keyValue.job_start_date > new DateTime(1753, 1, 1) && keyValue.job_start_date < new DateTime(9999, 1, 1))
                            {
                                validDate = true;
                            }
                            var boroughParametar = !String.IsNullOrEmpty(keyValue.borough) ? new SqlParameter("borough", keyValue.borough) : new SqlParameter("borough", DBNull.Value);
                            var blockParametar = !String.IsNullOrEmpty(keyValue.block) ? new SqlParameter("block", keyValue.block) : new SqlParameter("block", DBNull.Value);
                            var lotParametar = !String.IsNullOrEmpty(keyValue.lot) ? new SqlParameter("lot", keyValue.lot) : new SqlParameter("lot", DBNull.Value);
                            var job_start_dateParametar = validDate ? new SqlParameter("job_start_date", keyValue.job_start_date) : new SqlParameter("job_start_date", DBNull.Value);
                            var job_typeParametar = !String.IsNullOrEmpty(keyValue.job_type) ? new SqlParameter("job_type", keyValue.job_type) : new SqlParameter("job_type", DBNull.Value);
                            var work_typeParametar = !String.IsNullOrEmpty(keyValue.work_type) ? new SqlParameter("work_type", keyValue.work_type) : new SqlParameter("work_type", DBNull.Value);
                            var bbl_10_digitsParametar = !String.IsNullOrEmpty(bbl_10_digits) ? new SqlParameter("bbl_10_digits", bbl_10_digits) : new SqlParameter("bbl_10_digits", DBNull.Value);
                            var dobrundateParametar = keyValue.dobrundate.HasValue ? new SqlParameter("dobrundate", keyValue.dobrundate) : new SqlParameter("dobrundate", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertPermit @borough, @block, @lot, @job_start_date, @job_type, @work_type, @bbl_10_digits, @dobrundate ",
                                boroughParametar, blockParametar, lotParametar, job_start_dateParametar, job_typeParametar, work_typeParametar, bbl_10_digitsParametar, dobrundateParametar);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
        public void InsertAllEvictions(DateTime? EXECUTED_DATE)
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<SocrataEvictions>(GlobalVariables.EvictionID);

            IEnumerable<SocrataEvictions> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                if (EXECUTED_DATE.HasValue)
                {
                    var strDate = EXECUTED_DATE.Value.Year + "-" + EXECUTED_DATE.Value.Month + "-" + EXECUTED_DATE.Value.Day;
                    var soql = new SoqlQuery()
                          .Where("EXECUTED_DATE > '" + strDate + "' AND EXECUTED_DATE < '2030-01-01'")
                          .Limit(myLimit)
                          .Offset(myOffset);
                    rows = dataset.Query<SocrataEvictions>(soql);
                }
                else
                {
                    rows = dataset.GetRows(limit: myLimit, offset: myOffset);
                }
                if (rows.Count() == 0)
                {
                    break;
                }
                else
                {
                    foreach (var keyValue in rows)
                    {
                        using (var ctx = new NYC_Web_Mapping_AppEntities())
                        {
                            var COURT_INDEX_NUMBERParametar = !String.IsNullOrEmpty(keyValue.COURT_INDEX_NUMBER) ? new SqlParameter("COURT_INDEX_NUMBER", keyValue.COURT_INDEX_NUMBER) : new SqlParameter("COURT_INDEX_NUMBER", DBNull.Value);
                            var DOCKET_NUMBERParametar = !String.IsNullOrEmpty(keyValue.DOCKET_NUMBER) ? new SqlParameter("DOCKET_NUMBER", keyValue.DOCKET_NUMBER) : new SqlParameter("DOCKET_NUMBER", DBNull.Value);
                            var EVICTION_ADDRESSParametar = !String.IsNullOrEmpty(keyValue.EVICTION_ADDRESS) ? new SqlParameter("EVICTION_ADDRESS", keyValue.EVICTION_ADDRESS) : new SqlParameter("EVICTION_ADDRESS", DBNull.Value);
                            var EVICTION_APT_NUMParametar = !String.IsNullOrEmpty(keyValue.EVICTION_APT_NUM) ? new SqlParameter("EVICTION_APT_NUM", keyValue.EVICTION_APT_NUM) : new SqlParameter("EVICTION_APT_NUM", DBNull.Value);
                            var EXECUTED_DATEParametar = keyValue.EXECUTED_DATE.HasValue ? new SqlParameter("EXECUTED_DATE", keyValue.EXECUTED_DATE) : new SqlParameter("EXECUTED_DATE", DBNull.Value);
                            var MARSHAL_FIRST_NAMEParametar = !String.IsNullOrEmpty(keyValue.MARSHAL_FIRST_NAME) ? new SqlParameter("MARSHAL_FIRST_NAME", keyValue.MARSHAL_FIRST_NAME) : new SqlParameter("MARSHAL_FIRST_NAME", DBNull.Value);
                            var MARSHAL_LAST_NAMEParametar = !String.IsNullOrEmpty(keyValue.MARSHAL_LAST_NAME) ? new SqlParameter("MARSHAL_LAST_NAME", keyValue.MARSHAL_LAST_NAME) : new SqlParameter("MARSHAL_LAST_NAME", DBNull.Value);
                            var RESIDENTIAL_COMMERCIAL_INDParametar = !String.IsNullOrEmpty(keyValue.RESIDENTIAL_COMMERCIAL_IND) ? new SqlParameter("RESIDENTIAL_COMMERCIAL_IND", keyValue.RESIDENTIAL_COMMERCIAL_IND) : new SqlParameter("RESIDENTIAL_COMMERCIAL_IND", DBNull.Value);
                            var BOROUGHParametar = !String.IsNullOrEmpty(keyValue.BOROUGH) ? new SqlParameter("BOROUGH", keyValue.BOROUGH) : new SqlParameter("BOROUGH", DBNull.Value);
                            var EVICTION_ZIPParametar = !String.IsNullOrEmpty(keyValue.EVICTION_ZIP) ? new SqlParameter("EVICTION_ZIP", keyValue.EVICTION_ZIP) : new SqlParameter("EVICTION_ZIP", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertEviction @COURT_INDEX_NUMBER, @DOCKET_NUMBER, @EVICTION_ADDRESS, @EVICTION_APT_NUM, @EXECUTED_DATE, @MARSHAL_FIRST_NAME, @MARSHAL_LAST_NAME, @RESIDENTIAL_COMMERCIAL_IND, @BOROUGH, @EVICTION_ZIP ",
                                COURT_INDEX_NUMBERParametar, DOCKET_NUMBERParametar, EVICTION_ADDRESSParametar, EVICTION_APT_NUMParametar, EXECUTED_DATEParametar,
                                MARSHAL_FIRST_NAMEParametar, MARSHAL_LAST_NAMEParametar, RESIDENTIAL_COMMERCIAL_INDParametar, BOROUGHParametar, EVICTION_ZIPParametar);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
        public void InsertAllViolations(string issue_date)
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<SocrataViolations>(GlobalVariables.ViolationID);

            IEnumerable<SocrataViolations> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                if (!String.IsNullOrEmpty(issue_date))
                {
                    var strDate = issue_date.Substring(0, 4) + "-" + issue_date.Substring(4, 2) + "-" + issue_date.Substring(6, 2);
                    var soql = new SoqlQuery()
                          .Where("issue_date > '" + strDate + "' AND issue_date LIKE '20%'")
                          .Limit(myLimit)
                          .Offset(myOffset);
                    rows = dataset.Query<SocrataViolations>(soql);
                }
                else
                {
                    rows = dataset.GetRows(limit: myLimit, offset: myOffset);
                }
                if (rows.Count() == 0)
                {
                    break;
                }
                else
                {
                    foreach (var keyValue in rows)
                    {
                        string bbl_10_digits = "";
                        if (!String.IsNullOrEmpty(keyValue.boro) && !String.IsNullOrEmpty(keyValue.block) && !String.IsNullOrEmpty(keyValue.lot))
                        {
                            bbl_10_digits = keyValue.boro + keyValue.block + keyValue.lot.Substring(1);
                        }
                        using (var ctx = new NYC_Web_Mapping_AppEntities())
                        {
                            var isn_dob_bis_violParametar = !String.IsNullOrEmpty(keyValue.isn_dob_bis_viol) ? new SqlParameter("isn_dob_bis_viol", keyValue.isn_dob_bis_viol) : new SqlParameter("isn_dob_bis_viol", DBNull.Value);
                            var boroParametar = !String.IsNullOrEmpty(keyValue.boro) ? new SqlParameter("boro", keyValue.boro) : new SqlParameter("boro", DBNull.Value);
                            var binParametar = !String.IsNullOrEmpty(keyValue.bin) ? new SqlParameter("bin", keyValue.bin) : new SqlParameter("bin", DBNull.Value);
                            var blockParametar = !String.IsNullOrEmpty(keyValue.block) ? new SqlParameter("block", keyValue.block) : new SqlParameter("block", DBNull.Value);
                            var lotParametar = !String.IsNullOrEmpty(keyValue.lot) ? new SqlParameter("lot", keyValue.lot) : new SqlParameter("lot", DBNull.Value);
                            var issue_dateParametar = !String.IsNullOrEmpty(keyValue.issue_date) ? new SqlParameter("issue_date", keyValue.issue_date) : new SqlParameter("issue_date", DBNull.Value);
                            var violation_type_codeParametar = !String.IsNullOrEmpty(keyValue.violation_type_code) ? new SqlParameter("violation_type_code", keyValue.violation_type_code) : new SqlParameter("violation_type_code", DBNull.Value);
                            var violation_numberParametar = !String.IsNullOrEmpty(keyValue.violation_number) ? new SqlParameter("violation_number", keyValue.violation_number) : new SqlParameter("violation_number", DBNull.Value);
                            var house_numberParametar = !String.IsNullOrEmpty(keyValue.house_number) ? new SqlParameter("house_number", keyValue.house_number) : new SqlParameter("house_number", DBNull.Value);
                            var streetParametar = !String.IsNullOrEmpty(keyValue.street) ? new SqlParameter("street", keyValue.street) : new SqlParameter("street", DBNull.Value);
                            var disposition_dateParametar = !String.IsNullOrEmpty(keyValue.disposition_date) ? new SqlParameter("disposition_date", keyValue.disposition_date) : new SqlParameter("disposition_date", DBNull.Value);
                            var disposition_commentsParametar = !String.IsNullOrEmpty(keyValue.disposition_comments) ? new SqlParameter("disposition_comments", keyValue.disposition_comments) : new SqlParameter("disposition_comments", DBNull.Value);
                            var device_numberParametar = !String.IsNullOrEmpty(keyValue.device_number) ? new SqlParameter("device_number", keyValue.device_number) : new SqlParameter("device_number", DBNull.Value);
                            var descriptionParametar = !String.IsNullOrEmpty(keyValue.description) ? new SqlParameter("description", keyValue.description) : new SqlParameter("description", DBNull.Value);
                            var ecb_numberParametar = !String.IsNullOrEmpty(keyValue.ecb_number) ? new SqlParameter("ecb_number", keyValue.ecb_number) : new SqlParameter("ecb_number", DBNull.Value);
                            var numberParametar = !String.IsNullOrEmpty(keyValue.number) ? new SqlParameter("number", keyValue.number) : new SqlParameter("number", DBNull.Value);
                            var violation_categoryParametar = !String.IsNullOrEmpty(keyValue.violation_category) ? new SqlParameter("violation_category", keyValue.violation_category) : new SqlParameter("violation_category", DBNull.Value);
                            var violation_typeParametar = !String.IsNullOrEmpty(keyValue.violation_type) ? new SqlParameter("violation_type", keyValue.violation_type) : new SqlParameter("violation_type", DBNull.Value);
                            var bbl_10_digitsParametar = !String.IsNullOrEmpty(bbl_10_digits) ? new SqlParameter("bbl_10_digits", bbl_10_digits) : new SqlParameter("bbl_10_digits", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertViolation @isn_dob_bis_viol, @boro, @bin, @block, @lot, @issue_date, @violation_type_code, @violation_number, @house_number, @street, @disposition_date, @disposition_comments, @device_number, @description, @ecb_number, @number, @violation_category, @violation_type, @bbl_10_digits ",
                                isn_dob_bis_violParametar, boroParametar, binParametar, blockParametar, lotParametar, issue_dateParametar, violation_type_codeParametar, violation_numberParametar,
                                house_numberParametar, streetParametar, disposition_dateParametar, disposition_commentsParametar, device_numberParametar,
                                descriptionParametar, ecb_numberParametar, numberParametar, violation_categoryParametar, violation_typeParametar, bbl_10_digitsParametar);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
        public DatabaseMaxValues GetMaxValues()
        {
            DatabaseMaxValues result = new DatabaseMaxValues();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                ctx.Database.CommandTimeout = 600;
                result = ctx.Database.SqlQuery<DatabaseMaxValues>("EXEC dbo.GetMaxValues ").FirstOrDefault();
            }
            return result;
        }
        public void CheckAlerts(int? maxOBJECTID)
        {
            List<MyAlert> lstAlerts = db.MyAlerts.Where(w => DbFunctions.TruncateTime(w.Next_DateCheck.Value) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
            foreach (MyAlert alert in lstAlerts)
            {
                string sqlQuery = alert.AlertQuery;
                string formatedDate = alert.Last_DateCheck.Value.ToString("yyyy'-'MM'-'dd");
                string formatedViolationDate = alert.Last_DateCheck.Value.ToString("yyyyMMdd");
                sqlQuery += " AND (";
                if (alert.Last_OBJECTID.HasValue)
                {
                    if (sqlQuery.EndsWith("("))
                    {
                        sqlQuery += " p.OBJECTID > " + alert.Last_OBJECTID.Value;
                    }
                    else
                    {
                        sqlQuery += " OR p.OBJECTID > " + alert.Last_OBJECTID.Value;
                    }
                    if (maxOBJECTID > alert.Last_OBJECTID.Value)
                    {
                        alert.Last_OBJECTID = maxOBJECTID;
                    }
                }
                if (alert.IsEnergySearch)
                {
                    if (sqlQuery.EndsWith("("))
                    {
                        sqlQuery += " en.generation_date > '" + formatedDate + "'";
                    }
                    else
                    {
                        sqlQuery += " OR en.generation_date > '" + formatedDate + "'";
                    }
                }
                if (alert.IsPermitSearch)
                {
                    if (sqlQuery.EndsWith("("))
                    {
                        sqlQuery += " pe.dobrundate > '" + formatedDate + "'";
                    }
                    else
                    {
                        sqlQuery += " OR pe.dobrundate > '" + formatedDate + "'";
                    }
                }
                if (alert.IsViolationSearch)
                {
                    if (sqlQuery.EndsWith("("))
                    {
                        sqlQuery += " (v.issue_date > '" + formatedViolationDate + "' AND v.issue_date LIKE '20%')";
                    }
                    else
                    {
                        sqlQuery += " OR (v.issue_date > '" + formatedDate + "' AND v.issue_date LIKE '20%')";
                    }
                }
                if (alert.IsEvictionSearch)
                {
                    if (sqlQuery.EndsWith("("))
                    {
                        sqlQuery += " (ev.EXECUTED_DATE > '" + formatedDate + "' AND ev.EXECUTED_DATE < '2030-01-01')";
                    }
                    else
                    {
                        sqlQuery += " OR (ev.EXECUTED_DATE > '" + formatedDate + "' AND ev.EXECUTED_DATE < '2030-01-01')";
                    }
                }
                sqlQuery += ")";
                List<DatabaseAttributes> newSearch = SearchDatabase(sqlQuery);
                if (newSearch.Count > 0)
                {
                    alert.IsUnread = true;
                    SendMail(alert.Username);
                }
                else
                {
                    alert.IsUnread = false;
                }
                alert.Last_DateCheck = DateTime.Now;
                alert.Next_DateCheck = DateTime.Now.AddDays(alert.Frequency);
                db.Entry(alert).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public List<DatabaseAttributes> SearchDatabase(string sqlQuery)
        {
            List<DatabaseAttributes> returnResult = new List<DatabaseAttributes>();
            using (var ctx = new NYC_Web_Mapping_AppEntities())
            {
                returnResult = ctx.Database.SqlQuery<DatabaseAttributes>(sqlQuery).ToList();
            }
            return returnResult;
        }
        public bool SendMail(string userEmail)
        {
            string subject = "You have new alert in NYC REI application";
            string body = "<p>Visit <a href='http://13.92.226.170:3000/'>NYC REI application</a> and see new data based on your created alert</p>";
            try
            {
                MailAddress from = new MailAddress(Properties.EmailSender, "NYC REI");
                MailAddress to = new MailAddress(userEmail);
                MailMessage mailMessage = new MailMessage(from, to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                new SmtpClient
                {
                    Host = Properties.SMTPClient,
                    Port = Convert.ToInt32(Properties.SMTPPort),
                    EnableSsl = true,
                    Credentials = new NetworkCredential(Properties.EmailSender, Properties.Password)
                }.Send(mailMessage);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
