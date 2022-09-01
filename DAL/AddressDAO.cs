using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
    public class AddressDAO 
    {
        public int AddAddress(Address address)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.Addresses.Add(address);
                    db.SaveChanges();
                    return address.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            
        }

        public List<AddressDTO> GetAddressList()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<AddressDTO> list = new List<AddressDTO>();

                List<Address> addresses = db.Addresses.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();

                foreach (var address in addresses)
                {
                    AddressDTO dto = new AddressDTO();
                    dto.ID = address.ID;
                    dto.AddressContent = address.Address1;
                    dto.Email = address.Email;
                    dto.Phone = address.Phone;
                    dto.Phone2 = address.Phone2;
                    dto.Fax = address.Fax;
                    dto.LargeMapPath = address.MapPathLarge;
                    dto.SmallMapPath = address.MapPathSmall;

                    list.Add(dto);
                }
                return list;
            }
           
        }

       

        public void UpdateAddress(AddressDTO model)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Address address = db.Addresses.First(x => x.ID == model.ID);
                    address.Address1 = model.AddressContent;
                    address.Email = model.Email;
                    address.Phone = model.Phone;
                    address.Phone2 = model.Phone2;
                    address.Fax = model.Fax;
                    address.MapPathLarge = model.LargeMapPath;
                    address.MapPathSmall = model.SmallMapPath;

                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }

    

        public AddressDTO GetUserWithID(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    AddressDTO addressDTO = new AddressDTO();
                    Address address = db.Addresses.First(x => x.ID == ID);
                    addressDTO.ID = address.ID;
                    addressDTO.AddressContent = address.Address1;
                    addressDTO.Email = address.Email;
                    addressDTO.Phone = address.Phone;
                    addressDTO.Phone2 = address.Phone2;
                    addressDTO.Fax = address.Fax;
                    addressDTO.LargeMapPath = address.MapPathLarge;
                    addressDTO.SmallMapPath = address.MapPathSmall;
                    return addressDTO;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }

        public void DeleteAddress(int ID)
        {
            using(POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Address address = db.Addresses.First(x => x.ID == ID);
                    address.isDeleted = true;
                    address.DeletedDate = DateTime.Now;
                    address.LastUpdateDate = DateTime.Now;
                    address.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }
    }
}
