using NYC_REI_Console.Entities;
using NYC_REI_Console.Helpers;
using NYC_REI_Console.Models;
using SODA;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
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

        public void InsertAllMapPluto(int? OBJECTID)
        {
            int minObjectID = OBJECTID.HasValue ? OBJECTID.Value : 0;
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
                    string urlPost = "https://services5.arcgis.com/GfwWNkhOj9bNBqoJ/arcgis/rest/services/MAPPLUTO/FeatureServer/0/query?where=" + whereClaues + "&f=pjson&returnGeometry=false&outFields=OBJECTID,BBL,Borough,Address,ZipCode,Latitude,Longitude,BldgArea,ComArea,ResArea,NumFloors,UnitsRes,ZoneDist1,Overlay1,Overlay2,AssessTot,YearBuilt,OwnerName,BldgClass,CD,Tract2010,LandUse";
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
                                var Tract2010Parametar = !String.IsNullOrEmpty(tf.attributes.Tract2010) ? new SqlParameter("Tract2010", tf.attributes.Tract2010) : new SqlParameter("Tract2010", DBNull.Value);
                                var LandUseParametar = !String.IsNullOrEmpty(tf.attributes.LandUse) ? new SqlParameter("LandUse", tf.attributes.LandUse) : new SqlParameter("LandUse", DBNull.Value);

                                ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertMapPluto @OBJECTID, @BBL, @Borough, @Address, @ZipCode, @Latitude, @Longitude, @BldgArea, @ComArea, @ResArea, @NumFloors, @UnitsRes, @ZoneDist1, @Overlay1, @Overlay2, @AssessTot, @YearBuilt, @OwnerName, @BldgClass, @CD, @Tract2010, @LandUse ",
                                    OBJECTIDParametar, BBLParametar, BoroughParametar, AddressParametar, ZipCodeParametar, LatitudeParametar, LongitudeParametar
                                    , BldgAreaParametar, ComAreaParametar, ResAreaParametar, NumFloorsParametar, UnitsResParametar, ZoneDist1Parametar, Overlay1Parametar
                                    , Overlay2Parametar, AssessTotParametar, YearBuiltParametar, OwnerNameParametar, BldgClassParametar, CDParametar, Tract2010Parametar, LandUseParametar);

                                //ctx.Database.ExecuteSqlCommand("EXEC dbo.UpdateMapPluto @OBJECTID, @CD ",
                                //    OBJECTIDParametar, CDParametar);
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
                            var dobrundateParametar = GlobalVariables.ToNullableDateTime(keyValue.dobrundate) != null ? new SqlParameter("dobrundate", keyValue.dobrundate) : new SqlParameter("dobrundate", DBNull.Value);
                            var job__Parametar = !String.IsNullOrEmpty(keyValue.job__) ? new SqlParameter("job__", keyValue.job__) : new SqlParameter("job__", DBNull.Value);
                            var permittee_s_first_nameParametar = !String.IsNullOrEmpty(keyValue.permittee_s_first_name) ? new SqlParameter("permittee_s_first_name", keyValue.permittee_s_first_name) : new SqlParameter("permittee_s_first_name", DBNull.Value);
                            var permittee_s_last_nameParametar = !String.IsNullOrEmpty(keyValue.permittee_s_last_name) ? new SqlParameter("permittee_s_last_name", keyValue.permittee_s_last_name) : new SqlParameter("permittee_s_last_name", DBNull.Value);
                            var permittee_s_business_nameParametar = !String.IsNullOrEmpty(keyValue.permittee_s_business_name) ? new SqlParameter("permittee_s_business_name", keyValue.permittee_s_business_name) : new SqlParameter("permittee_s_business_name", DBNull.Value);
                            var permittee_s_license_typeParametar = !String.IsNullOrEmpty(keyValue.permittee_s_license_type) ? new SqlParameter("permittee_s_license_type", keyValue.permittee_s_license_type) : new SqlParameter("permittee_s_license_type", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertPermit @borough, @block, @lot, @job_start_date, @job_type, @work_type, @bbl_10_digits, @dobrundate, @job__, @permittee_s_first_name, @permittee_s_last_name, @permittee_s_business_name, @permittee_s_license_type ",
                                boroughParametar, blockParametar, lotParametar, job_start_dateParametar, job_typeParametar, work_typeParametar, bbl_10_digitsParametar, dobrundateParametar, job__Parametar, permittee_s_first_nameParametar, permittee_s_last_nameParametar, permittee_s_business_nameParametar, permittee_s_license_typeParametar);
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
        public void InsertAllDistricts(int? DistrictOBJECTID)
        {
            int minObjectID = DistrictOBJECTID.HasValue ? DistrictOBJECTID.Value + 1 : 0;
            JsonDistrictData data = new JsonDistrictData();
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            int step = 100;
            int ObjectIDmin = minObjectID + 1;
            int ObjectIDmax = step + minObjectID + 1;
            while (true)
            {
                using (var client = new WebClient())
                {
                    string whereClaues = "objectid >= " + ObjectIDmin + " AND objectid < " + ObjectIDmax;
                    ObjectIDmin = ObjectIDmax;
                    ObjectIDmax += step;
                    string urlPost = "https://services.arcgis.com/uKN48PkxmWiqJM9q/ArcGIS/rest/services/DSNY_Districts_OFFICIAL/FeatureServer/0/query?where=" + whereClaues + "&f=pjson&returnGeometry=false&outFields=*";
                    var json = client.DownloadString(urlPost);

                    data = serializer.Deserialize<JsonDistrictData>(json);

                    if (data.features.Count() == 0)
                    {
                        break;
                    }
                    else
                    {
                        foreach (TableFieldsDistrict tf in data.features)
                        {
                            using (var ctx = new NYC_Web_Mapping_AppEntities())
                            {
                                var OBJECTIDParametar = new SqlParameter("OBJECTID", tf.attributes.OBJECTID);
                                var DISTRICTCODEParametar = new SqlParameter("DISTRICTCODE", tf.attributes.DISTRICTCODE);
                                var DISTRICTParametar = !String.IsNullOrEmpty(tf.attributes.DISTRICT) ? new SqlParameter("DISTRICT", tf.attributes.DISTRICT) : new SqlParameter("DISTRICT", DBNull.Value);

                                ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertDistrict @OBJECTID, @DISTRICTCODE, @DISTRICT ",
                                    OBJECTIDParametar, DISTRICTCODEParametar, DISTRICTParametar);
                            }
                        }
                    }
                }
            }
        }
        public void InsertAllElevators(DateTime? filing_date)
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<SocrataElevators>(GlobalVariables.ElevatorID);

            IEnumerable<SocrataElevators> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                if (filing_date.HasValue)
                {
                    var strDate = filing_date.Value.Year + "-" + filing_date.Value.Month + "-" + filing_date.Value.Day;
                    var soql = new SoqlQuery()
                          .Where("filing_date > '" + strDate + "'")
                          .Limit(myLimit)
                          .Offset(myOffset);
                    rows = dataset.Query<SocrataElevators>(soql);
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

                            var job_filing_numberParameter = !String.IsNullOrEmpty(keyValue.job_filing_number) ? new SqlParameter("job_filing_number", keyValue.job_filing_number) : new SqlParameter("job_filing_number", DBNull.Value);
                            var job_numberParameter = !String.IsNullOrEmpty(keyValue.job_number) ? new SqlParameter("job_number", keyValue.job_number) : new SqlParameter("job_number", DBNull.Value);
                            var filing_numberParameter = !String.IsNullOrEmpty(keyValue.filing_number) ? new SqlParameter("filing_number", keyValue.filing_number) : new SqlParameter("filing_number", DBNull.Value);
                            var filing_dateParameter = keyValue.filing_date.HasValue ? new SqlParameter("filing_date", keyValue.filing_date) : new SqlParameter("filing_date", DBNull.Value);
                            var filing_typeParameter = !String.IsNullOrEmpty(keyValue.filing_type) ? new SqlParameter("filing_type", keyValue.filing_type) : new SqlParameter("filing_type", DBNull.Value);
                            var elevatordevicetypeParameter = !String.IsNullOrEmpty(keyValue.elevatordevicetype) ? new SqlParameter("elevatordevicetype", keyValue.elevatordevicetype) : new SqlParameter("elevatordevicetype", DBNull.Value);
                            var filing_statusParameter = !String.IsNullOrEmpty(keyValue.filing_status) ? new SqlParameter("filing_status", keyValue.filing_status) : new SqlParameter("filing_status", DBNull.Value);
                            var filingstatus_or_filingincludesParameter = !String.IsNullOrEmpty(keyValue.filingstatus_or_filingincludes) ? new SqlParameter("filingstatus_or_filingincludes", keyValue.filingstatus_or_filingincludes) : new SqlParameter("filingstatus_or_filingincludes", DBNull.Value);
                            var building_codeParameter = !String.IsNullOrEmpty(keyValue.building_code) ? new SqlParameter("building_code", keyValue.building_code) : new SqlParameter("building_code", DBNull.Value);
                            var electrical_permit_numberParameter = !String.IsNullOrEmpty(keyValue.electrical_permit_number) ? new SqlParameter("electrical_permit_number", keyValue.electrical_permit_number) : new SqlParameter("electrical_permit_number", DBNull.Value);
                            var binParameter = !String.IsNullOrEmpty(keyValue.bin) ? new SqlParameter("bin", keyValue.bin) : new SqlParameter("bin", DBNull.Value);
                            var house_numberParameter = !String.IsNullOrEmpty(keyValue.house_number) ? new SqlParameter("house_number", keyValue.house_number) : new SqlParameter("house_number", DBNull.Value);
                            var street_nameParameter = !String.IsNullOrEmpty(keyValue.street_name) ? new SqlParameter("street_name", keyValue.street_name) : new SqlParameter("street_name", DBNull.Value);
                            var zipParameter = !String.IsNullOrEmpty(keyValue.zip) ? new SqlParameter("zip", keyValue.zip) : new SqlParameter("zip", DBNull.Value);
                            var boroughParameter = !String.IsNullOrEmpty(keyValue.borough) ? new SqlParameter("borough", keyValue.borough) : new SqlParameter("borough", DBNull.Value);
                            var blockParameter = !String.IsNullOrEmpty(keyValue.block) ? new SqlParameter("block", keyValue.block) : new SqlParameter("block", DBNull.Value);
                            var lotParameter = !String.IsNullOrEmpty(keyValue.lot) ? new SqlParameter("lot", keyValue.lot) : new SqlParameter("lot", DBNull.Value);
                            var building_typeParameter = !String.IsNullOrEmpty(keyValue.building_type) ? new SqlParameter("building_type", keyValue.building_type) : new SqlParameter("building_type", DBNull.Value);
                            var buildingstoriesParameter = !String.IsNullOrEmpty(keyValue.buildingstories) ? new SqlParameter("buildingstories", keyValue.buildingstories) : new SqlParameter("buildingstories", DBNull.Value);
                            var bblParameter = !String.IsNullOrEmpty(keyValue.bbl) ? new SqlParameter("bbl", keyValue.bbl) : new SqlParameter("bbl", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertElevator @job_filing_number, @job_number, @filing_number, @filing_date, @filing_type, @elevatordevicetype, @filing_status, @filingstatus_or_filingincludes, @building_code, @electrical_permit_number, @bin, @house_number, @street_name, @zip, @borough, @block, @lot, @building_type, @buildingstories, @bbl",
                               job_filing_numberParameter, job_numberParameter, filing_numberParameter, filing_dateParameter, filing_typeParameter, elevatordevicetypeParameter, filing_statusParameter, filingstatus_or_filingincludesParameter, building_codeParameter, electrical_permit_numberParameter, binParameter, house_numberParameter, street_nameParameter, zipParameter, boroughParameter, blockParameter, lotParameter, building_typeParameter, buildingstoriesParameter, bblParameter);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
        public void InsertAllPropertySales(DateTime? sale_date)
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<SocrataPropertySales>(GlobalVariables.PropertySaleID);

            IEnumerable<SocrataPropertySales> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                if (sale_date.HasValue)
                {
                    var strDate = sale_date.Value.Year + "-" + sale_date.Value.Month + "-" + sale_date.Value.Day;
                    var soql = new SoqlQuery()
                          .Where("sale_date > '" + strDate + "'")
                          .Limit(myLimit)
                          .Offset(myOffset);
                    rows = dataset.Query<SocrataPropertySales>(soql);
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
                            var boroughParameter = keyValue.borough.HasValue ? new SqlParameter("borough", keyValue.borough) : new SqlParameter("borough", DBNull.Value);
                            var neighborhoodParameter = !String.IsNullOrEmpty(keyValue.neighborhood) ? new SqlParameter("neighborhood", keyValue.neighborhood) : new SqlParameter("neighborhood", DBNull.Value);
                            var building_class_categoryParameter = !String.IsNullOrEmpty(keyValue.building_class_category) ? new SqlParameter("building_class_category", keyValue.building_class_category) : new SqlParameter("building_class_category", DBNull.Value);
                            var tax_class_at_presentParameter = !String.IsNullOrEmpty(keyValue.tax_class_at_present) ? new SqlParameter("tax_class_at_present", keyValue.tax_class_at_present) : new SqlParameter("tax_class_at_present", DBNull.Value);
                            var blockParameter = keyValue.block.HasValue ? new SqlParameter("block", keyValue.block) : new SqlParameter("block", DBNull.Value);
                            var lotParameter = keyValue.lot.HasValue ? new SqlParameter("lot", keyValue.lot) : new SqlParameter("lot", DBNull.Value);
                            var ease_mentParameter = !String.IsNullOrEmpty(keyValue.ease_ment) ? new SqlParameter("ease_ment", keyValue.ease_ment) : new SqlParameter("ease_ment", DBNull.Value);
                            var building_class_at_presentParameter = !String.IsNullOrEmpty(keyValue.building_class_at_present) ? new SqlParameter("building_class_at_present", keyValue.building_class_at_present) : new SqlParameter("building_class_at_present", DBNull.Value);
                            var addressParameter = !String.IsNullOrEmpty(keyValue.address) ? new SqlParameter("address", keyValue.address) : new SqlParameter("address", DBNull.Value);
                            var apartment_numberParameter = !String.IsNullOrEmpty(keyValue.apartment_number) ? new SqlParameter("apartment_number", keyValue.apartment_number) : new SqlParameter("apartment_number", DBNull.Value);
                            var zip_codeParameter = keyValue.zip_code.HasValue ? new SqlParameter("zip_code", keyValue.zip_code) : new SqlParameter("zip_code", DBNull.Value);
                            var residential_unitsParameter = keyValue.residential_units.HasValue ? new SqlParameter("residential_units", keyValue.residential_units) : new SqlParameter("residential_units", DBNull.Value);
                            var commercial_unitsParameter = keyValue.commercial_units.HasValue ? new SqlParameter("commercial_units", keyValue.commercial_units) : new SqlParameter("commercial_units", DBNull.Value);
                            var total_unitsParameter = keyValue.total_units.HasValue ? new SqlParameter("total_units", keyValue.total_units) : new SqlParameter("total_units", DBNull.Value);
                            var land_square_feetParameter = !String.IsNullOrEmpty(keyValue.land_square_feet) ? new SqlParameter("land_square_feet", keyValue.land_square_feet) : new SqlParameter("land_square_feet", DBNull.Value);
                            var gross_square_feetParameter = keyValue.gross_square_feet.HasValue ? new SqlParameter("gross_square_feet", keyValue.gross_square_feet) : new SqlParameter("gross_square_feet", DBNull.Value);
                            var year_builtParameter = keyValue.year_built.HasValue ? new SqlParameter("year_built", keyValue.year_built) : new SqlParameter("year_built", DBNull.Value);
                            var tax_class_at_time_of_saleParameter = keyValue.tax_class_at_time_of_sale.HasValue ? new SqlParameter("tax_class_at_time_of_sale", keyValue.tax_class_at_time_of_sale) : new SqlParameter("tax_class_at_time_of_sale", DBNull.Value);
                            var building_class_at_time_ofParameter = !String.IsNullOrEmpty(keyValue.building_class_at_time_of) ? new SqlParameter("building_class_at_time_of", keyValue.building_class_at_time_of) : new SqlParameter("building_class_at_time_of", DBNull.Value);
                            var sale_priceParameter = !String.IsNullOrEmpty(keyValue.sale_price) ? new SqlParameter("sale_price", keyValue.sale_price) : new SqlParameter("sale_price", DBNull.Value);
                            var sale_dateParameter = keyValue.sale_date.HasValue ? new SqlParameter("sale_date", keyValue.sale_date) : new SqlParameter("sale_date", DBNull.Value);
                            var bblParameter = !String.IsNullOrEmpty(keyValue.bbl) ? new SqlParameter("bbl", keyValue.bbl) : new SqlParameter("bbl", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertPropertySale @borough, @neighborhood, @building_class_category, @tax_class_at_present, @block, @lot, @ease_ment, @building_class_at_present, @address, @apartment_number, @zip_code, @residential_units, @commercial_units, @total_units, @land_square_feet, @gross_square_feet, @year_built, @tax_class_at_time_of_sale, @building_class_at_time_of, @sale_price, @sale_date, @bbl",
                               boroughParameter, neighborhoodParameter, building_class_categoryParameter, tax_class_at_presentParameter, blockParameter, lotParameter, ease_mentParameter, building_class_at_presentParameter, addressParameter, apartment_numberParameter, zip_codeParameter, residential_unitsParameter, commercial_unitsParameter, total_unitsParameter, land_square_feetParameter, gross_square_feetParameter, year_builtParameter, tax_class_at_time_of_saleParameter, building_class_at_time_ofParameter, sale_priceParameter, sale_dateParameter, bblParameter);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
        public void InsertAllEcbViolations(string ecb_issue_date)
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<SocrataEcbViolations>(GlobalVariables.EcbViolationID);

            IEnumerable<SocrataEcbViolations> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                if (!String.IsNullOrEmpty(ecb_issue_date))
                {
                    var strDate = ecb_issue_date.Substring(0, 4) + "-" + ecb_issue_date.Substring(4, 2) + "-" + ecb_issue_date.Substring(6, 2);
                    var soql = new SoqlQuery()
                          .Where("issue_date > '" + strDate + "' AND issue_date LIKE '20%'")
                          .Limit(myLimit)
                          .Offset(myOffset);
                    rows = dataset.Query<SocrataEcbViolations>(soql);
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
                        if (!String.IsNullOrEmpty(keyValue.BORO) && !String.IsNullOrEmpty(keyValue.BLOCK) && !String.IsNullOrEmpty(keyValue.LOT))
                        {
                            string myLot = keyValue.LOT.Count() == 5 ? keyValue.LOT.Substring(1) : keyValue.LOT;
                            bbl_10_digits = keyValue.BORO + keyValue.BLOCK + myLot;
                        }
                        using (var ctx = new NYC_Web_Mapping_AppEntities())
                        {
                            var ISN_DOB_BIS_EXTRACTParametar = !String.IsNullOrEmpty(keyValue.ISN_DOB_BIS_EXTRACT) ? new SqlParameter("ISN_DOB_BIS_EXTRACT", keyValue.ISN_DOB_BIS_EXTRACT) : new SqlParameter("ISN_DOB_BIS_EXTRACT", DBNull.Value);
                            var ECB_VIOLATION_NUMBERParametar = !String.IsNullOrEmpty(keyValue.ECB_VIOLATION_NUMBER) ? new SqlParameter("ECB_VIOLATION_NUMBER", keyValue.ECB_VIOLATION_NUMBER) : new SqlParameter("ECB_VIOLATION_NUMBER", DBNull.Value);
                            var ECB_VIOLATION_STATUSParametar = !String.IsNullOrEmpty(keyValue.ECB_VIOLATION_STATUS) ? new SqlParameter("ECB_VIOLATION_STATUS", keyValue.ECB_VIOLATION_STATUS) : new SqlParameter("ECB_VIOLATION_STATUS", DBNull.Value);
                            var DOB_VIOLATION_NUMBERParametar = !String.IsNullOrEmpty(keyValue.DOB_VIOLATION_NUMBER) ? new SqlParameter("DOB_VIOLATION_NUMBER", keyValue.DOB_VIOLATION_NUMBER) : new SqlParameter("DOB_VIOLATION_NUMBER", DBNull.Value);
                            var BINParametar = !String.IsNullOrEmpty(keyValue.BIN) ? new SqlParameter("BIN", keyValue.BIN) : new SqlParameter("BIN", DBNull.Value);
                            var BOROParametar = !String.IsNullOrEmpty(keyValue.BORO) ? new SqlParameter("BORO", keyValue.BORO) : new SqlParameter("BORO", DBNull.Value);
                            var BLOCKParametar = !String.IsNullOrEmpty(keyValue.BLOCK) ? new SqlParameter("BLOCK", keyValue.BLOCK) : new SqlParameter("BLOCK", DBNull.Value);
                            var LOTParametar = !String.IsNullOrEmpty(keyValue.LOT) ? new SqlParameter("LOT", keyValue.LOT) : new SqlParameter("LOT", DBNull.Value);
                            var HEARING_DATEParametar = !String.IsNullOrEmpty(keyValue.HEARING_DATE) ? new SqlParameter("HEARING_DATE", keyValue.HEARING_DATE) : new SqlParameter("HEARING_DATE", DBNull.Value);
                            var HEARING_TIMEParametar = !String.IsNullOrEmpty(keyValue.HEARING_TIME) ? new SqlParameter("HEARING_TIME", keyValue.HEARING_TIME) : new SqlParameter("HEARING_TIME", DBNull.Value);
                            var SERVED_DATEParametar = !String.IsNullOrEmpty(keyValue.SERVED_DATE) ? new SqlParameter("SERVED_DATE", keyValue.SERVED_DATE) : new SqlParameter("SERVED_DATE", DBNull.Value);
                            var ISSUE_DATEParametar = !String.IsNullOrEmpty(keyValue.ISSUE_DATE) ? new SqlParameter("ISSUE_DATE", keyValue.ISSUE_DATE) : new SqlParameter("ISSUE_DATE", DBNull.Value);
                            var SEVERITYParametar = !String.IsNullOrEmpty(keyValue.SEVERITY) ? new SqlParameter("SEVERITY", keyValue.SEVERITY) : new SqlParameter("SEVERITY", DBNull.Value);
                            var VIOLATION_TYPEParametar = !String.IsNullOrEmpty(keyValue.VIOLATION_TYPE) ? new SqlParameter("VIOLATION_TYPE", keyValue.VIOLATION_TYPE) : new SqlParameter("VIOLATION_TYPE", DBNull.Value);
                            var RESPONDENT_NAMEParametar = !String.IsNullOrEmpty(keyValue.RESPONDENT_NAME) ? new SqlParameter("RESPONDENT_NAME", keyValue.RESPONDENT_NAME) : new SqlParameter("RESPONDENT_NAME", DBNull.Value);
                            var RESPONDENT_HOUSE_NUMBERParametar = !String.IsNullOrEmpty(keyValue.RESPONDENT_HOUSE_NUMBER) ? new SqlParameter("RESPONDENT_HOUSE_NUMBER", keyValue.RESPONDENT_HOUSE_NUMBER) : new SqlParameter("RESPONDENT_HOUSE_NUMBER", DBNull.Value);
                            var RESPONDENT_STREETParametar = !String.IsNullOrEmpty(keyValue.RESPONDENT_STREET) ? new SqlParameter("RESPONDENT_STREET", keyValue.RESPONDENT_STREET) : new SqlParameter("RESPONDENT_STREET", DBNull.Value);
                            var RESPONDENT_CITYParametar = !String.IsNullOrEmpty(keyValue.RESPONDENT_CITY) ? new SqlParameter("RESPONDENT_CITY", keyValue.RESPONDENT_CITY) : new SqlParameter("RESPONDENT_CITY", DBNull.Value);
                            var RESPONDENT_ZIPParametar = !String.IsNullOrEmpty(keyValue.RESPONDENT_ZIP) ? new SqlParameter("RESPONDENT_ZIP", keyValue.RESPONDENT_ZIP) : new SqlParameter("RESPONDENT_ZIP", DBNull.Value);
                            var VIOLATION_DESCRIPTIONParametar = !String.IsNullOrEmpty(keyValue.VIOLATION_DESCRIPTION) ? new SqlParameter("VIOLATION_DESCRIPTION", keyValue.VIOLATION_DESCRIPTION) : new SqlParameter("VIOLATION_DESCRIPTION", DBNull.Value);
                            var PENALITY_IMPOSEDParametar = !String.IsNullOrEmpty(keyValue.PENALITY_IMPOSED) ? new SqlParameter("PENALITY_IMPOSED", keyValue.PENALITY_IMPOSED) : new SqlParameter("PENALITY_IMPOSED", DBNull.Value);
                            var AMOUNT_PAIDParametar = !String.IsNullOrEmpty(keyValue.AMOUNT_PAID) ? new SqlParameter("AMOUNT_PAID", keyValue.AMOUNT_PAID) : new SqlParameter("AMOUNT_PAID", DBNull.Value);
                            var BALANCE_DUEParametar = !String.IsNullOrEmpty(keyValue.BALANCE_DUE) ? new SqlParameter("BALANCE_DUE", keyValue.BALANCE_DUE) : new SqlParameter("BALANCE_DUE", DBNull.Value);
                            var AGGRAVATED_LEVELParametar = !String.IsNullOrEmpty(keyValue.AGGRAVATED_LEVEL) ? new SqlParameter("AGGRAVATED_LEVEL", keyValue.AGGRAVATED_LEVEL) : new SqlParameter("AGGRAVATED_LEVEL", DBNull.Value);
                            var HEARING_STATUSParametar = !String.IsNullOrEmpty(keyValue.HEARING_STATUS) ? new SqlParameter("HEARING_STATUS", keyValue.HEARING_STATUS) : new SqlParameter("HEARING_STATUS", DBNull.Value);
                            var CERTIFICATION_STATUSParametar = !String.IsNullOrEmpty(keyValue.CERTIFICATION_STATUS) ? new SqlParameter("CERTIFICATION_STATUS", keyValue.CERTIFICATION_STATUS) : new SqlParameter("CERTIFICATION_STATUS", DBNull.Value);
                            var bbl_10_digitsParametar = !String.IsNullOrEmpty(bbl_10_digits) ? new SqlParameter("bbl_10_digits", bbl_10_digits) : new SqlParameter("bbl_10_digits", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertEcbViolation @ISN_DOB_BIS_EXTRACT, @ECB_VIOLATION_NUMBER, @ECB_VIOLATION_STATUS, @DOB_VIOLATION_NUMBER, @BIN, @BORO, @BLOCK, @LOT, @HEARING_DATE, @HEARING_TIME, @SERVED_DATE, @ISSUE_DATE, @SEVERITY, @VIOLATION_TYPE, @RESPONDENT_NAME, @RESPONDENT_HOUSE_NUMBER, @RESPONDENT_STREET, @RESPONDENT_CITY, @RESPONDENT_ZIP, @VIOLATION_DESCRIPTION, @PENALITY_IMPOSED, @AMOUNT_PAID, @BALANCE_DUE, @AGGRAVATED_LEVEL, @HEARING_STATUS, @CERTIFICATION_STATUS, @bbl_10_digits ",
                                ISN_DOB_BIS_EXTRACTParametar, ECB_VIOLATION_NUMBERParametar, ECB_VIOLATION_STATUSParametar, DOB_VIOLATION_NUMBERParametar, BINParametar, BOROParametar, BLOCKParametar, LOTParametar, HEARING_DATEParametar,
                                HEARING_TIMEParametar, SERVED_DATEParametar, ISSUE_DATEParametar, SEVERITYParametar, VIOLATION_TYPEParametar, RESPONDENT_NAMEParametar, RESPONDENT_HOUSE_NUMBERParametar, RESPONDENT_STREETParametar, RESPONDENT_CITYParametar,
                                RESPONDENT_ZIPParametar, VIOLATION_DESCRIPTIONParametar, PENALITY_IMPOSEDParametar, AMOUNT_PAIDParametar, BALANCE_DUEParametar, AGGRAVATED_LEVELParametar, HEARING_STATUSParametar, CERTIFICATION_STATUSParametar, bbl_10_digitsParametar);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
        public void InsertAllDesignations(int? OBJECTID)
        {
            int minObjectID = OBJECTID.HasValue ? OBJECTID.Value : 0;
            JsonMapDesignationsData data = new JsonMapDesignationsData();
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            int step = 10000;
            int ObjectIDmin = minObjectID + 1;
            int ObjectIDmax = step + minObjectID + 1;
            while (true)
            {
                using (var client = new WebClient())
                {
                    string whereClaues = "objectid >= " + ObjectIDmin + " AND objectid < " + ObjectIDmax;
                    ObjectIDmin = ObjectIDmax;
                    ObjectIDmax += step;
                    string urlPost = "https://services5.arcgis.com/GfwWNkhOj9bNBqoJ/arcgis/rest/services/nyedesignations/FeatureServer/0/query?where=" + whereClaues + "&f=pjson&returnGeometry=false&outFields=*";
                    var json = client.DownloadString(urlPost);

                    data = serializer.Deserialize<JsonMapDesignationsData>(json);

                    if (data.features.Count() == 0)
                    {
                        break;
                    }
                    else
                    {
                        foreach (TableFieldsMapDesignations tf in data.features)
                        {
                            using (var ctx = new NYC_Web_Mapping_AppEntities())
                            {
                                var OBJECTIDParametar = new SqlParameter("OBJECTID", tf.attributes.OBJECTID);
                                var ENUMBERParametar = !String.IsNullOrEmpty(tf.attributes.ENUMBER) ? new SqlParameter("ENUMBER", tf.attributes.ENUMBER) : new SqlParameter("ENUMBER", DBNull.Value);
                                var CEQR_NUMParametar = !String.IsNullOrEmpty(tf.attributes.CEQR_NUM) ? new SqlParameter("CEQR_NUM", tf.attributes.CEQR_NUM) : new SqlParameter("CEQR_NUM", DBNull.Value);
                                var ULURP_NUMParametar = !String.IsNullOrEmpty(tf.attributes.ULURP_NUM) ? new SqlParameter("ULURP_NUM", tf.attributes.ULURP_NUM) : new SqlParameter("ULURP_NUM", DBNull.Value);
                                var BOROCODEParametar = tf.attributes.BOROCODE.HasValue ? new SqlParameter("BOROCODE", tf.attributes.BOROCODE) : new SqlParameter("BOROCODE", DBNull.Value);
                                var TAXBLOCKParametar = tf.attributes.TAXBLOCK.HasValue ? new SqlParameter("TAXBLOCK", tf.attributes.TAXBLOCK) : new SqlParameter("TAXBLOCK", DBNull.Value);
                                var TAXLOTParametar = tf.attributes.TAXLOT.HasValue ? new SqlParameter("TAXLOT", tf.attributes.TAXLOT) : new SqlParameter("TAXLOT", DBNull.Value);
                                var ZONING_MAPParametar = !String.IsNullOrEmpty(tf.attributes.ZONING_MAP) ? new SqlParameter("ZONING_MAP", tf.attributes.ZONING_MAP) : new SqlParameter("ZONING_MAP", DBNull.Value);
                                var DESCRIPTIONParametar = !String.IsNullOrEmpty(tf.attributes.DESCRIPTION) ? new SqlParameter("DESCRIPTION", tf.attributes.DESCRIPTION) : new SqlParameter("DESCRIPTION", DBNull.Value);
                                var BBLParametar = !String.IsNullOrEmpty(tf.attributes.BBL) ? new SqlParameter("BBL", tf.attributes.BBL) : new SqlParameter("BBL", DBNull.Value);

                                ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertDesignation @OBJECTID, @ENUMBER, @CEQR_NUM, @ULURP_NUM, @BOROCODE, @TAXBLOCK, @TAXLOT, @ZONING_MAP, @DESCRIPTION, @BBL ",
                                    OBJECTIDParametar, ENUMBERParametar, CEQR_NUMParametar, ULURP_NUMParametar, BOROCODEParametar, TAXBLOCKParametar, TAXLOTParametar
                                    , ZONING_MAPParametar, DESCRIPTIONParametar, BBLParametar);
                            }
                        }
                    }
                }
            }
        }
        public void InsertAllSafetyFacadesComplianceFilings(DateTime? filing_date_sfcf)
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<SocrataSafetyFacadesComplianceFilings>(GlobalVariables.SafetyFacadesComplianceFilingsID);

            IEnumerable<SocrataSafetyFacadesComplianceFilings> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                if (filing_date_sfcf.HasValue)
                {
                    var strDate = filing_date_sfcf.Value.Year + "-" + filing_date_sfcf.Value.Month + "-" + filing_date_sfcf.Value.Day;
                    var soql = new SoqlQuery()
                          .Where("filing_date > '" + strDate + "'")
                          .Limit(myLimit)
                          .Offset(myOffset);
                    rows = dataset.Query<SocrataSafetyFacadesComplianceFilings>(soql);
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
                            bbl_10_digits = boroNum + keyValue.block.PadLeft(5, '0') + keyValue.lot.PadLeft(4, '0');
                        }
                        using (var ctx = new NYC_Web_Mapping_AppEntities())
                        {
                            var addressParametar = !String.IsNullOrEmpty(keyValue.street_name) ? new SqlParameter("address", keyValue.house_no + " " + keyValue.street_name) : new SqlParameter("address", DBNull.Value);
                            var boroughParametar = !String.IsNullOrEmpty(keyValue.borough) ? new SqlParameter("borough", keyValue.borough) : new SqlParameter("borough", DBNull.Value);
                            var blockParametar = !String.IsNullOrEmpty(keyValue.block) ? new SqlParameter("block", keyValue.block) : new SqlParameter("block", DBNull.Value);
                            var lotParametar = !String.IsNullOrEmpty(keyValue.lot) ? new SqlParameter("lot", keyValue.lot) : new SqlParameter("lot", DBNull.Value);
                            var qewi_nameParametar = !String.IsNullOrEmpty(keyValue.qewi_name) ? new SqlParameter("qewi_name", keyValue.qewi_name) : new SqlParameter("qewi_name", DBNull.Value);
                            var qewi_bus_nameParametar = !String.IsNullOrEmpty(keyValue.qewi_bus_name) ? new SqlParameter("qewi_bus_name", keyValue.qewi_bus_name) : new SqlParameter("qewi_bus_name", DBNull.Value);
                            var owner_nameParametar = !String.IsNullOrEmpty(keyValue.owner_name) ? new SqlParameter("owner_name", keyValue.owner_name) : new SqlParameter("owner_name", DBNull.Value);
                            var owner_bus_nameParametar = !String.IsNullOrEmpty(keyValue.owner_bus_name) ? new SqlParameter("owner_bus_name", keyValue.owner_bus_name) : new SqlParameter("owner_bus_name", DBNull.Value);
                            var filing_dateParametar = keyValue.filing_date.HasValue ? new SqlParameter("filing_date", keyValue.filing_date) : new SqlParameter("filing_date", DBNull.Value);
                            var filing_statusParametar = !String.IsNullOrEmpty(keyValue.filing_status) ? new SqlParameter("filing_status", keyValue.filing_status) : new SqlParameter("filing_status", DBNull.Value);
                            var bbl_10_digitsParametar = !String.IsNullOrEmpty(bbl_10_digits) ? new SqlParameter("bbl_10_digits", bbl_10_digits) : new SqlParameter("bbl_10_digits", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertSafetyFacadesComplianceFilings @address, @borough, @block, @lot, @qewi_name, @qewi_bus_name, @owner_name, @owner_bus_name, @filing_date, @filing_status, @bbl_10_digits ",
                                addressParametar, boroughParametar, blockParametar, lotParametar, qewi_nameParametar, qewi_bus_nameParametar, owner_nameParametar, owner_bus_nameParametar, filing_dateParametar, filing_statusParametar, bbl_10_digitsParametar);
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
                if (alert.IsElevatorSearch)
                {
                    if (sqlQuery.EndsWith("("))
                    {
                        sqlQuery += " (el.filing_date > '" + formatedDate + "' AND el.filing_date < '2030-01-01')";
                    }
                    else
                    {
                        sqlQuery += " OR (el.filing_date > '" + formatedDate + "' AND el.filing_date < '2030-01-01')";
                    }
                }
                if (alert.IsPropertySalesSearch)
                {
                    if (sqlQuery.EndsWith("("))
                    {
                        sqlQuery += " (el.sale_date > '" + formatedDate + "' AND el.sale_date < '2030-01-01')";
                    }
                    else
                    {
                        sqlQuery += " OR (el.sale_date > '" + formatedDate + "' AND el.sale_date < '2030-01-01')";
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
        public void InsertAllConsumerProfiles(int year)
        {
            List<JsonConsumerProfiles> result = new List<JsonConsumerProfiles>();
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            string states = "36"; //New York
            string county = "005,047,061,081,085"; //Bronx(2), Brooklyn(3), Manhattan(1), Queens(4), Staten Island(5)
            string variables = "DP05_0001E,DP05_0002E,DP05_0002PE,DP05_0003E,DP05_0003PE,DP05_0004E,DP05_0009E,DP05_0010E,DP05_0011E,DP05_0012E,DP05_0013E,DP05_0014E,DP05_0015E,DP05_0016E" +
            ",DP02_0001E,DP02_0001PE,DP02_0002E,DP02_0002PE,DP02_0003E,DP02_0003PE,DP02_0006E,DP02_0006PE,DP02_0008E,DP02_0008PE,DP02_0010E,DP02_0010PE,DP02_0011E,DP02_0011PE,DP02_0012E,DP02_0012PE,DP02_0024E,DP02_0024PE,DP02_0025E,DP02_0025PE,DP02_0026E,DP02_0026PE,DP02_0027E,DP02_0027PE,DP02_0028E,DP02_0028PE,DP02_0029E,DP02_0029PE,DP02_0030E,DP02_0030PE,DP02_0031E,DP02_0031PE,DP02_0032E,DP02_0032PE,DP02_0033E,DP02_0033PE,DP02_0034E,DP02_0034PE,DP02_0035E,DP02_0035PE,DP02_0052E,DP02_0052PE,DP02_0053E,DP02_0053PE,DP02_0054E,DP02_0054PE,DP02_0055E,DP02_0055PE,DP02_0056E,DP02_0056PE,DP02_0057E,DP02_0057PE,DP02_0058E,DP02_0058PE,DP02_0059E,DP02_0059PE,DP02_0060E,DP02_0060PE,DP02_0061E,DP02_0061PE,DP02_0062E,DP02_0062PE,DP02_0063E,DP02_0063PE,DP02_0064E,DP02_0064PE,DP02_0070E,DP02_0070PE,DP02_0071E,DP02_0071PE,DP02_0074E,DP02_0074PE,DP02_0075E,DP02_0075PE,DP02_0076E,DP02_0076PE,DP02_0078E,DP02_0078PE,DP02_0079E,DP02_0079PE,DP02_0080E,DP02_0080PE,DP02_0081E,DP02_0081PE,DP02_0082E,DP02_0082PE,DP02_0083E,DP02_0083PE,DP02_0084E,DP02_0084PE,DP02_0150E,DP02_0150PE,DP02_0151E,DP02_0151PE,DP02_0152E,DP02_0152PE" +
            ",DP03_0001E,DP03_0001PE,DP03_0003E,DP03_0003PE,DP03_0004E,DP03_0004PE,DP03_0005E,DP03_0005PE,DP03_0007E,DP03_0007PE,DP03_0009E,DP03_0009PE,DP03_0010E,DP03_0010PE,DP03_0012E,DP03_0012PE,DP03_0013E,DP03_0013PE,DP03_0051E,DP03_0051PE,DP03_0052E,DP03_0052PE,DP03_0053E,DP03_0053PE,DP03_0054E,DP03_0054PE,DP03_0055E,DP03_0055PE,DP03_0056E,DP03_0056PE,DP03_0057E,DP03_0057PE,DP03_0058E,DP03_0058PE,DP03_0059E,DP03_0059PE,DP03_0060E,DP03_0060PE,DP03_0061E,DP03_0061PE,DP03_0063E,DP03_0063PE,DP03_0066E,DP03_0066PE,DP03_0068E,DP03_0068PE,DP03_0069E,DP03_0069PE,DP03_0070E,DP03_0070PE,DP03_0071E,DP03_0071PE,DP03_0072E,DP03_0072PE,DP03_0073E,DP03_0073PE,DP03_0079E,DP03_0079PE,DP03_0080E,DP03_0080PE,DP03_0081E,DP03_0081PE,DP03_0082E,DP03_0082PE,DP03_0083E,DP03_0083PE,DP03_0084E,DP03_0084PE,DP03_0085E,DP03_0085PE,DP03_0095E,DP03_0095PE,DP03_0096E,DP03_0096PE,DP03_0097E,DP03_0097PE,DP03_0098E,DP03_0098PE,DP03_0099E,DP03_0099PE,DP03_0102E,DP03_0102PE,DP03_0103E,DP03_0103PE,DP03_0104E,DP03_0104PE,DP03_0105E,DP03_0105PE,DP03_0106E,DP03_0106PE,DP03_0107E,DP03_0107PE,DP03_0108E,DP03_0108PE,DP03_0109E,DP03_0109PE,DP03_0110E,DP03_0110PE,DP03_0111E,DP03_0111PE,DP03_0112E,DP03_0112PE,DP03_0113E,DP03_0113PE,DP03_0133E,DP03_0133PE,DP03_0134E,DP03_0134PE,DP03_0135E,DP03_0135PE" +
            ",DP04_0006E,DP04_0006PE,DP04_0007E,DP04_0007PE,DP04_0008E,DP04_0008PE,DP04_0009E,DP04_0009PE,DP04_0010E,DP04_0010PE,DP04_0011E,DP04_0011PE,DP04_0012E,DP04_0012PE,DP04_0013E,DP04_0013PE,DP04_0014E,DP04_0014PE,DP04_0027E,DP04_0027PE,DP04_0028E,DP04_0028PE,DP04_0029E,DP04_0029PE,DP04_0030E,DP04_0030PE,DP04_0031E,DP04_0031PE,DP04_0032E,DP04_0032PE,DP04_0033E,DP04_0033PE,DP04_0034E,DP04_0034PE,DP04_0035E,DP04_0035PE,DP04_0036E,DP04_0036PE,DP04_0037E,DP04_0037PE,DP04_0038E,DP04_0038PE,DP04_0039E,DP04_0039PE,DP04_0040E,DP04_0040PE,DP04_0041E,DP04_0041PE,DP04_0042E,DP04_0042PE,DP04_0043E,DP04_0043PE,DP04_0044E,DP04_0044PE,DP04_0045E,DP04_0045PE,DP04_0046E,DP04_0046PE,DP04_0047E,DP04_0047PE,DP04_0050E,DP04_0050PE,DP04_0051E,DP04_0051PE,DP04_0052E,DP04_0052PE,DP04_0053E,DP04_0053PE,DP04_0054E,DP04_0054PE,DP04_0055E,DP04_0055PE,DP04_0056E,DP04_0056PE,DP04_0076E,DP04_0076PE,DP04_0077E,DP04_0077PE,DP04_0078E,DP04_0078PE,DP04_0079E,DP04_0079PE,DP04_0090E,DP04_0090PE,DP04_0091E,DP04_0091PE,DP04_0092E,DP04_0092PE,DP04_0093E,DP04_0093PE,DP04_0094E,DP04_0094PE,DP04_0095E,DP04_0095PE,DP04_0096E,DP04_0096PE,DP04_0097E,DP04_0097PE,DP04_0098E,DP04_0098PE,DP04_0099E,DP04_0099PE,DP04_0100E,DP04_0100PE,DP04_0101E,DP04_0101PE,DP04_0110E,DP04_0110PE,DP04_0111E,DP04_0111PE,DP04_0112E,DP04_0112PE,DP04_0113E,DP04_0113PE,DP04_0114E,DP04_0114PE,DP04_0115E,DP04_0115PE,DP04_0126E,DP04_0126PE,DP04_0127E,DP04_0127PE,DP04_0128E,DP04_0128PE,DP04_0129E,DP04_0129PE,DP04_0130E,DP04_0130PE,DP04_0131E,DP04_0131PE,DP04_0132E,DP04_0132PE,DP04_0133E,DP04_0133PE,DP04_0134E,DP04_0134PE,DP04_0136E,DP04_0136PE,DP04_0137E,DP04_0137PE,DP04_0138E,DP04_0138PE,DP04_0139E,DP04_0139PE,DP04_0140E,DP04_0140PE,DP04_0141E,DP04_0141PE,DP04_0142E,DP04_0142PE";
            //string variables = "DP03_0079E,DP03_0079PE,DP03_0080E,DP03_0080PE,DP03_0081E,DP03_0081PE,DP03_0082E,DP03_0082PE,DP03_0083E,DP03_0083PE,DP03_0084E,DP03_0084PE,DP03_0085E,DP03_0085PE";
            string[] lstVariables = variables.Split(',');
            foreach (string variable in lstVariables)
            {
                using (var client = new WebClient())
                {
                    string urlPost = "https://api.census.gov/data/" + year + "/acs/acs5/profile?get=" + variable + "&for=tract:*&in=state:" + states + "&in=county:" + county;
                    var json = client.DownloadString(urlPost);
                    var rawData = serializer.Deserialize<string[][]>(json);
                    bool header = true;
                    foreach (string[] item in rawData)
                    {
                        if (header)
                        {
                            header = false;
                        }
                        else
                        {
                            string MyValue = item[0];
                            string State = item[1];
                            string County = item[2];
                            string Tract = item[3];
                            using (var ctx = new NYC_Web_Mapping_AppEntities())
                            {
                                var MyAttributeParametar = new SqlParameter("MyAttribute", variable);
                                var MyValueParametar = new SqlParameter("MyValue", MyValue);
                                var StateParametar = new SqlParameter("State", State);
                                var CountyParametar = new SqlParameter("County", County);
                                var TractParametar = new SqlParameter("Tract", Tract);
                                var YearParametar = new SqlParameter("Year", year.ToString());
                                ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertUpdateConsumerProfiles @MyAttribute, @MyValue, @State, @County, @Tract, @Year ",
                                    MyAttributeParametar, MyValueParametar, StateParametar, CountyParametar, TractParametar, YearParametar);
                            }
                        }
                    }
                }
            }
        }
        public void ReadTextFile()
        {
            List<string> quartiles = new List<string>() { "qtr1", "qtr2", "qtr3", "qtr4" };
            for (int i = 1993; i <= 2020; i++)
            {
                foreach (string item in quartiles)
                {
                    string filename = i + item + ".txt.txt";
                    var lines = File.ReadLines("E:\\master files\\" + filename);
                    foreach (string line in lines)
                    {
                        string[] splitLines = line.Split('|');
                        int CIK = Convert.ToInt32(splitLines[0]);
                        string CompanyName = splitLines[1];
                        string FormType = splitLines[2];
                        DateTime DateFiled = Convert.ToDateTime(splitLines[3]);
                        string Filename = splitLines[4];
                        if (FormType == "S-1")
                        {
                            using (var ctx = new NYC_Web_Mapping_AppEntities())
                            {
                                var CIKParameter = new SqlParameter("CIK", CIK);
                                var CompanyNameParameter = new SqlParameter("CompanyName", CompanyName);
                                var FormTypeParameter = new SqlParameter("FormType", FormType);
                                var DateFiledParameter = new SqlParameter("DateFiled", DateFiled);
                                var FilenameParameter = new SqlParameter("Filename", Filename);

                                ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertSecFilings @CIK, @CompanyName, @FormType, @DateFiled, @Filename",
                                   CIKParameter, CompanyNameParameter, FormTypeParameter, DateFiledParameter, FilenameParameter);
                            }
                        }
                    }
                }
            }
        }
        public void DownloadAllTextFiles()
        {
            foreach (SecFiling item in db.SecFilings.Where(w => w.DateFiled.Year >= 2019))
            {
                int year = item.DateFiled.Year;
                string filename = item.Filename.Substring(item.Filename.LastIndexOf('/') + 1, item.Filename.Length - item.Filename.LastIndexOf('/') - 1);
                using (var client = new WebClient())
                {
                    if (!Directory.Exists("E:\\SecFillings\\" + year))
                    {
                        Directory.CreateDirectory("E:\\SecFillings\\" + year);
                    }
                    client.DownloadFile("https://www.sec.gov/Archives/" + item.Filename, "E:\\SecFillings\\" + year + "\\" + filename);
                }
            }
        }
        public void DownloadBls()
        {
            JsonBlsData data = new JsonBlsData();
            var serializer = new JavaScriptSerializer();
            using (var client = new WebClient())
            {
                //List<string> lstSeries = new List<string>() { "APU000070111", "CUUR0000SA0L1E", "NCU5306633300003", "EBU401KINC0000ML", "CCU010000100000P", "CEU0800000003", "WMU40000011020000004130992500", "PRS85006032", "IPUBN212___W000", "EIUCOCANMANU" };
                List<string> lstSeries = new List<string>() { "EBU401KINC0000ML", "CCU010000100000P", "CEU0800000003", "WMU40000011020000004130992500", "PRS85006032", "IPUBN212___W000", "EIUCOCANMANU" };
                foreach (string item in lstSeries)
                {
                    for (int i = 1990; i <= 2020; i++)
                    {
                        var values = new NameValueCollection();
                        values.Add("seriesid", item);
                        values.Add("startyear", i.ToString());
                        values.Add("endyear", i.ToString());
                        values.Add("registrationKey", GlobalVariables.BlsRegistrationKey);
                        string urlPost = "https://api.bls.gov/publicAPI/v2/timeseries/data/";

                        var response = client.UploadValues(urlPost, values);
                        string responseString = Encoding.Default.GetString(response);
                        data = serializer.Deserialize<JsonBlsData>(responseString);
                        if (data.Results.series != null && data.Results.series.FirstOrDefault().data.Count > 0)
                        {
                            foreach (BlsData insertData in data.Results.series.FirstOrDefault().data)
                            {
                                using (var ctx = new NYC_Web_Mapping_AppEntities())
                                {
                                    var DataSeriesIDParametar = new SqlParameter("DataSeriesID", item);
                                    var YearParametar = new SqlParameter("Year", insertData.year);
                                    var PeriodParametar = new SqlParameter("Period", insertData.period);
                                    var PeriodNameParametar = new SqlParameter("PeriodName", insertData.periodName);
                                    var ValueParametar = new SqlParameter("Value", insertData.value);
                                    ctx.Database.ExecuteSqlCommand("EXEC dbo.InsertBlsSeries @DataSeriesID, @Year, @Period, @PeriodName, @Value ",
                                        DataSeriesIDParametar, YearParametar, PeriodParametar, PeriodNameParametar, ValueParametar);
                                }
                            }
                        }
                    }
                }
            }
            ////Post request example version 2.0
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.bls.gov/publicAPI/v2/timeseries/data/");
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";
            ////Using Javascript Serializer
            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    var jS = new JavaScriptSerializer();
            //    List<string> lstSeries = new List<string>() { "CEU0800000003" };
            //    var newJson = jS.Serialize(new SeriesPost()
            //    {
            //        seriesid = lstSeries,
            //        startyear = "1990",
            //        endyear = "2020",
            //        catalog = true,
            //        calculations = true,
            //        annualaverage = true,
            //        registrationKey = GlobalVariables.BlsRegistrationKey
            //    });
            //    //View the JSON output
            //    System.Diagnostics.Debug.WriteLine(newJson);
            //    streamWriter.Write(newJson);
            //    streamWriter.Flush();
            //    streamWriter.Close();
            //}
        }

        public void hpd_contacts_insert()
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<hpd_contacts_ent>(GlobalVariables.hpd_contacts_ID);

            IEnumerable<hpd_contacts_ent> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                rows = dataset.GetRows(limit: myLimit, offset: myOffset);
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

                            var RegistrationContactIDParameter = !String.IsNullOrEmpty(keyValue.RegistrationContactID) ? new SqlParameter("RegistrationContactID", keyValue.RegistrationContactID) : new SqlParameter("RegistrationContactID", DBNull.Value);
                            var RegistrationIDParameter = !String.IsNullOrEmpty(keyValue.RegistrationID) ? new SqlParameter("RegistrationID", keyValue.RegistrationID) : new SqlParameter("RegistrationID", DBNull.Value);
                            var TypeParameter = !String.IsNullOrEmpty(keyValue.Type) ? new SqlParameter("Type", keyValue.Type) : new SqlParameter("Type", DBNull.Value);
                            var ContactDescriptionParameter = !String.IsNullOrEmpty(keyValue.ContactDescription) ? new SqlParameter("ContactDescription", keyValue.ContactDescription) : new SqlParameter("ContactDescription", DBNull.Value);
                            var CorporationNameParameter = !String.IsNullOrEmpty(keyValue.CorporationName) ? new SqlParameter("CorporationName", keyValue.CorporationName) : new SqlParameter("CorporationName", DBNull.Value);
                            var TitleParameter = !String.IsNullOrEmpty(keyValue.Title) ? new SqlParameter("Title", keyValue.Title) : new SqlParameter("Title", DBNull.Value);
                            var FirstNameParameter = !String.IsNullOrEmpty(keyValue.FirstName) ? new SqlParameter("FirstName", keyValue.FirstName) : new SqlParameter("FirstName", DBNull.Value);
                            var MiddleInitialParameter = !String.IsNullOrEmpty(keyValue.MiddleInitial) ? new SqlParameter("MiddleInitial", keyValue.MiddleInitial) : new SqlParameter("MiddleInitial", DBNull.Value);
                            var LastNameParameter = !String.IsNullOrEmpty(keyValue.LastName) ? new SqlParameter("LastName", keyValue.LastName) : new SqlParameter("LastName", DBNull.Value);
                            var BusinessHouseNumberParameter = !String.IsNullOrEmpty(keyValue.BusinessHouseNumber) ? new SqlParameter("BusinessHouseNumber", keyValue.BusinessHouseNumber) : new SqlParameter("BusinessHouseNumber", DBNull.Value);
                            var BusinessStreetNameParameter = !String.IsNullOrEmpty(keyValue.BusinessStreetName) ? new SqlParameter("BusinessStreetName", keyValue.BusinessStreetName) : new SqlParameter("BusinessStreetName", DBNull.Value);
                            var BusinessApartmentParameter = !String.IsNullOrEmpty(keyValue.BusinessApartment) ? new SqlParameter("BusinessApartment", keyValue.BusinessApartment) : new SqlParameter("BusinessApartment", DBNull.Value);
                            var BusinessCityParameter = !String.IsNullOrEmpty(keyValue.BusinessCity) ? new SqlParameter("BusinessCity", keyValue.BusinessCity) : new SqlParameter("BusinessCity", DBNull.Value);
                            var BusinessStateParameter = !String.IsNullOrEmpty(keyValue.BusinessState) ? new SqlParameter("BusinessState", keyValue.BusinessState) : new SqlParameter("BusinessState", DBNull.Value);
                            var BusinessZipParameter = !String.IsNullOrEmpty(keyValue.BusinessZip) ? new SqlParameter("BusinessZip", keyValue.BusinessZip) : new SqlParameter("BusinessZip", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.hpd_contacts_insert @RegistrationContactID, @RegistrationID, @Type, @ContactDescription, @CorporationName, @Title, @FirstName, @MiddleInitial, @LastName, @BusinessHouseNumber, @BusinessStreetName, @BusinessApartment, @BusinessCity, @BusinessState, @BusinessZip",
                               RegistrationContactIDParameter, RegistrationIDParameter, TypeParameter, ContactDescriptionParameter, CorporationNameParameter, TitleParameter, FirstNameParameter, MiddleInitialParameter, LastNameParameter, BusinessHouseNumberParameter, BusinessStreetNameParameter, BusinessApartmentParameter, BusinessCityParameter, BusinessStateParameter, BusinessZipParameter);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
        public void hpd_registrations_insert()
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<hpd_registrations_ent>(GlobalVariables.hpd_registrations_ID);

            IEnumerable<hpd_registrations_ent> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                rows = dataset.GetRows(limit: myLimit, offset: myOffset);
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
                            var RegistrationIDParameter = !String.IsNullOrEmpty(keyValue.RegistrationID) ? new SqlParameter("RegistrationID", keyValue.RegistrationID) : new SqlParameter("RegistrationID", DBNull.Value);
                            var BuildingIDParameter = !String.IsNullOrEmpty(keyValue.BuildingID) ? new SqlParameter("BuildingID", keyValue.BuildingID) : new SqlParameter("BuildingID", DBNull.Value);
                            var BoroIDParameter = !String.IsNullOrEmpty(keyValue.BoroID) ? new SqlParameter("BoroID", keyValue.BoroID) : new SqlParameter("BoroID", DBNull.Value);
                            var BoroParameter = !String.IsNullOrEmpty(keyValue.Boro) ? new SqlParameter("Boro", keyValue.Boro) : new SqlParameter("Boro", DBNull.Value);
                            var HouseNumberParameter = !String.IsNullOrEmpty(keyValue.HouseNumber) ? new SqlParameter("HouseNumber", keyValue.HouseNumber) : new SqlParameter("HouseNumber", DBNull.Value);
                            var LowHouseNumberParameter = !String.IsNullOrEmpty(keyValue.LowHouseNumber) ? new SqlParameter("LowHouseNumber", keyValue.LowHouseNumber) : new SqlParameter("LowHouseNumber", DBNull.Value);
                            var HighHouseNumberParameter = !String.IsNullOrEmpty(keyValue.HighHouseNumber) ? new SqlParameter("HighHouseNumber", keyValue.HighHouseNumber) : new SqlParameter("HighHouseNumber", DBNull.Value);
                            var StreetNameParameter = !String.IsNullOrEmpty(keyValue.StreetName) ? new SqlParameter("StreetName", keyValue.StreetName) : new SqlParameter("StreetName", DBNull.Value);
                            var ZipParameter = !String.IsNullOrEmpty(keyValue.Zip) ? new SqlParameter("Zip", keyValue.Zip) : new SqlParameter("Zip", DBNull.Value);
                            var BlockParameter = keyValue.Block.HasValue ? new SqlParameter("Block", keyValue.Block) : new SqlParameter("Block", DBNull.Value);
                            var LotParameter = keyValue.Lot.HasValue ? new SqlParameter("Lot", keyValue.Lot) : new SqlParameter("Lot", DBNull.Value);
                            var BINParameter = keyValue.BIN.HasValue ? new SqlParameter("BIN", keyValue.BIN) : new SqlParameter("BIN", DBNull.Value);
                            var CommunityBoardParameter = keyValue.CommunityBoard.HasValue ? new SqlParameter("CommunityBoard", keyValue.CommunityBoard) : new SqlParameter("CommunityBoard", DBNull.Value);
                            var LastRegistrationDateParameter = keyValue.LastRegistrationDate.HasValue ? new SqlParameter("LastRegistrationDate", keyValue.LastRegistrationDate) : new SqlParameter("LastRegistrationDate", DBNull.Value);
                            var RegistrationEndDateParameter = keyValue.RegistrationEndDate.HasValue ? new SqlParameter("RegistrationEndDate", keyValue.RegistrationEndDate) : new SqlParameter("RegistrationEndDate", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.hpd_registrations_insert @RegistrationID, @BuildingID, @BoroID, @Boro, @HouseNumber, @LowHouseNumber, @HighHouseNumber, @StreetName, @Zip, @Block, @Lot, @BIN, @CommunityBoard, @LastRegistrationDate, @RegistrationEndDate",
                               RegistrationIDParameter, BuildingIDParameter, BoroIDParameter, BoroParameter, HouseNumberParameter, LowHouseNumberParameter, HighHouseNumberParameter, StreetNameParameter, ZipParameter, BlockParameter, LotParameter, BINParameter, CommunityBoardParameter, LastRegistrationDateParameter, RegistrationEndDateParameter);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
        public void hpd_violations_insert()
        {
            var client = new SodaClient("https://data.cityofnewyork.us", GlobalVariables.Token);

            // Get a reference to the resource itself
            // The result (a Resouce object) is a generic type
            // The type parameter represents the underlying rows of the resource
            // and can be any JSON-serializable class
            var dataset = client.GetResource<hpd_violations_ent>(GlobalVariables.hpd_violations_ID);

            IEnumerable<hpd_violations_ent> rows;
            int myLimit = 5000;
            int myOffset = 0;
            while (true)
            {
                rows = dataset.GetRows(limit: myLimit, offset: myOffset);
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
                            var ViolationIDParameter = keyValue.ViolationID.HasValue ? new SqlParameter("ViolationID", keyValue.ViolationID) : new SqlParameter("ViolationID", DBNull.Value);
                            var BuildingIDParameter = keyValue.BuildingID.HasValue ? new SqlParameter("BuildingID", keyValue.BuildingID) : new SqlParameter("BuildingID", DBNull.Value);
                            var RegistrationIDParameter = keyValue.RegistrationID.HasValue ? new SqlParameter("RegistrationID", keyValue.RegistrationID) : new SqlParameter("RegistrationID", DBNull.Value);
                            var ViolationStatusParameter = !String.IsNullOrEmpty(keyValue.ViolationStatus) ? new SqlParameter("ViolationStatus", keyValue.ViolationStatus) : new SqlParameter("ViolationStatus", DBNull.Value);
                            var BBLParameter = !String.IsNullOrEmpty(keyValue.BBL) ? new SqlParameter("BBL", keyValue.BBL) : new SqlParameter("BBL", DBNull.Value);

                            ctx.Database.ExecuteSqlCommand("EXEC dbo.hpd_violations_insert @ViolationID, @BuildingID, @RegistrationID, @ViolationStatus, @BBL ",
                               ViolationIDParameter, BuildingIDParameter, RegistrationIDParameter, ViolationStatusParameter, BBLParameter);
                        }
                    }
                }
                myOffset += myLimit;
            }
        }
    }
}
