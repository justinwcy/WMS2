using System.Reflection;

using Application.Constants;

using Infrastructure.Extensions.Identity.Authorization;

namespace Infrastructure.UserClaim
{
    public static class ClaimUtilities
    {
        public static Dictionary<string, string> GetClaimsFromRoles(string role)
        {
            switch (role)
            {
                case StaffRole.Admin:
                    return GetAdminClaims(DateTime.Now.AddYears(1000));
                case StaffRole.Manager:
                    return GetManagerClaims(DateTime.Now.AddYears(1000));
                case StaffRole.Supervisor:
                    return GetSupervisorClaims(DateTime.Now.AddYears(1000));
                case StaffRole.StockKeeper:
                    return GetStockKeeperClaims(DateTime.Now.AddYears(1000));
                case StaffRole.Receiver:
                    return GetReceiverClaims(DateTime.Now.AddYears(1000));
                case StaffRole.Purchaser:
                    return GetPurchaserClaims(DateTime.Now.AddYears(1000));
                case StaffRole.Picker:
                    return GetPickerClaims(DateTime.Now.AddYears(1000));
                case StaffRole.Packer:
                    return GetPackerClaims(DateTime.Now.AddYears(1000));
                case StaffRole.Vendor:
                    return GetVendorClaims(DateTime.Now.AddYears(1000));
                case StaffRole.QcInspector:
                    return GetQcInspectorClaims(DateTime.Now.AddYears(1000));
                case StaffRole.MasterControl:
                    return GetMasterControlClaims(DateTime.Now.AddYears(1000));
                default:
                    throw new ArgumentException($"{role} is not recognized");
            }
        }

        private static Dictionary<string, string> GetAdminClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Warehouse),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Warehouse),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Warehouse),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Warehouse),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Shop),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Shop),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Shop),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Shop),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetManagerClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Warehouse),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Warehouse),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Warehouse),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Warehouse),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Shop),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Shop),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Shop),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Shop),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetSupervisorClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Staff),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Rack),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Bin),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Courier),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.ProductGroup),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Shop),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Shop),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Shop),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Shop),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetStockKeeperClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrderProduct),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetReceiverClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrderProduct),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetPurchaserClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.IncomingOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Vendor),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.Customer),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrderProduct),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetPickerClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrderProduct),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetPackerClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrderProduct),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetVendorClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetQcInspectorClaims(DateTime expiryDate)
        {
            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            var claimsList = new List<string>()
            {
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Company),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.IncomingOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Inventory),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Zone),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.CustomerOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Create, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Delete, DataTableName.CustomerOrderDetail),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.Product),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrder),
                DataAccessUtilities.GetPolicyString(AccessName.Read, DataTableName.RefundOrderProduct),
                DataAccessUtilities.GetPolicyString(AccessName.Update, DataTableName.RefundOrderProduct),
            };

            foreach (var claim in claimsList)
            {
                claims.Add(claim, expiryDateString);
            }
            return claims;
        }

        private static Dictionary<string, string> GetMasterControlClaims(DateTime expiryDate)
        {
            var accessNames = typeof(AccessName)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => field.GetValue(null)?.ToString())
                .ToList();
            var dataTableNames = typeof(DataTableName)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => field.GetValue(null)?.ToString())
                .ToList();

            var claims = new Dictionary<string, string>();
            var expiryDateString = GetExpiryDateString(expiryDate);

            foreach (var accessName in accessNames)
            {
                if (accessName == null)
                {
                    continue;
                }

                foreach (var dataTableName in dataTableNames)
                {
                    if (dataTableName == null)
                    {
                        continue;
                    }
                    claims.Add(
                        DataAccessUtilities.GetPolicyString(accessName, dataTableName), expiryDateString);
                }
            }

            return claims;
        }

        private static string GetExpiryDateString(DateTime expiryDate)
        {
            return expiryDate.ToString("yyyy/MMM/dd HH:mm:ss");
        }
    }
}
