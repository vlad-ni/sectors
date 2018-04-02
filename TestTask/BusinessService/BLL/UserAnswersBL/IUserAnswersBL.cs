using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessService.BLL
{
    public interface IUserAnswersBL
    {
        UserAnswer GetUserAnswer(Guid userId);
        Guid AddUserAnswer(UserAnswer userAnswer);
        Guid UpdateUserAnswer(Guid userId, UserAnswer userAnswer);
    }
}
