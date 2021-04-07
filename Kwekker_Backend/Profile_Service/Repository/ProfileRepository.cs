using Profile_Service.DataAccess;
using Profile_Service.Interfaces;
using Profile_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile_Service.Repository
{
    public class ProfileRepository : IProfileRepository
    {

        private readonly ProfileDbContext _context;

        public ProfileRepository(ProfileDbContext context)
        {
            _context = context;
        }

        public bool create(Profile profile)
        {
            bool result;
            try
            {
                using (var context = _context)
                {
                    context.Profiles.Add(profile);
                    context.SaveChanges();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }        

        public Profile getByAccountID(int accountID)
        {
            Profile profile;
            try
            {
                using (var context = _context)
                {
                    profile = _context.Profiles
                        .Single(p => p.accountID == accountID);

                }
                return profile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public Profile getByProfileName(string profileName)
        {
            Profile profile;
            try
            {
                using (var context = _context)
                {
                    profile = _context.Profiles
                        .Single(p => p.profileName == profileName);

                }
                return profile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool checkUserTag(string userTag)
        {
            bool result;
            try
            {
                using (var context = _context)
                {
                    result = _context.Profiles.Any(p => p.userTag == userTag);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return result = false;
            }

            return result;
        }

        public bool update(Profile profile)
        {
            bool result;

            try
            {
                using (var context = _context)
                {
                    var prof = _context.Profiles
                        .Single(p => p.ID == profile.ID);
                    
                    prof.profileName = profile.profileName;
                    prof.userTag = profile.userTag;
                    prof.bio = profile.bio;
                    context.SaveChanges();
                }

                result = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }

        public bool delete(Profile profile)
        {
            bool result;

            try
            {
                using (var context = _context)
                {
                    var prof = _context.Profiles
                        .Single(p => p.ID == profile.ID && p.accountID == profile.accountID );

                    context.Profiles.Remove(prof);
                    context.SaveChanges();
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }
    }
}
