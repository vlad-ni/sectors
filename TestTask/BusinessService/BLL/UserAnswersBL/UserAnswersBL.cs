using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel.Context;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessService.BLL
{
    public class UserAnswersBL : IUserAnswersBL
    {
        private readonly IDomainDbContext _dbContext;

        public UserAnswersBL(IDomainDbContext domainDbContext)
        {
            _dbContext = domainDbContext;
        }

        public UserAnswer GetUserAnswer(Guid userId)
        {
            var userAnswers = _dbContext.UserAnswer.Include(x => x.UserSectors).FirstOrDefault(u => u.Id == userId);
            if (userAnswers == null)
                throw new ArgumentException($"User id {userId} does not exist");

            return userAnswers;
        }

        public Guid AddUserAnswer(UserAnswer userAnswer)
        {
            if (_dbContext.UserAnswer.Any(a => a.Id == userAnswer.Id))
                throw new ArgumentException($"User id {userAnswer.Id} already exists");

            userAnswer.UserSectors = GetUserNewSectors(userAnswer.Id, userAnswer.UserSectorCodes);

            _dbContext.UserAnswer.Add(userAnswer);
            _dbContext.SaveChanges();

            return userAnswer.Id;
        }

        public Guid UpdateUserAnswer(Guid userId, UserAnswer userAnswer)
        {
            userAnswer.Id = userId;
            var existingUser = _dbContext.UserAnswer.Include(x => x.UserSectors).Include("UserSectors.Sector").FirstOrDefault(a => a.Id == userAnswer.Id);
            if (existingUser == null)
                throw new ArgumentException($"User id {userAnswer.Id} does not exist");

            var removeSectors = existingUser.UserSectors.Where(s => !userAnswer.UserSectorCodes.Contains(s.Sector.Code));
            existingUser.UserSectors = existingUser.UserSectors.Except(removeSectors).ToList();

            var addSectors = GetUserNewSectors(userAnswer.Id, userAnswer.UserSectorCodes, existingUser);
            foreach(var s in addSectors)
            {
                existingUser.UserSectors.Add(s);
            }

            existingUser.Name = userAnswer.Name;
            existingUser.AgreedToTerms = userAnswer.AgreedToTerms;

            _dbContext.UserAnswer.Update(existingUser);
            _dbContext.SaveChanges();

            return userAnswer.Id;
        }

        private List<UserAnswerSectors> GetUserNewSectors(Guid userId, short[] userAnswerSectorCodes, UserAnswer existingUser = null)
        {
            var userSectors = new List<UserAnswerSectors>();
            var sectors = _dbContext.Sector.Where(s => userAnswerSectorCodes.Contains(s.Code)).ToList();
            foreach (var sector in sectors)
            {
                if (existingUser == null || !existingUser.UserSectors.Any(s => s.Sector.Code == sector.Code))
                {
                    userSectors.Add(new UserAnswerSectors { UserId = userId, SectorId = sector.Id });
                }
            }
            return userSectors;
        }
    }
}
