﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderListAPI.Models;

namespace WanderListAPI.Data
{
    public class DataSeed
    {
        public ApplicationUser GenerateApplicationUser()
        {
            var user = new ApplicationUser()
            {
                FirstName = "Norville",
                LastName = "Rogers",
                Id = Guid.NewGuid().ToString(),
                UserName = "Shaggy",
                Email = "surfer69@scoobydoo.com",
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "1234"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            return user;
        }

        public Content GenerateContent()
        {
            var content = new Content()
            {
                ContentId = Guid.NewGuid()
            };

            return content;
        }

        public Reward GenerateReward()
        {
            var reward = new Reward()
            {
                RewardId = Guid.NewGuid(),
                Name = "Burger King Coupon",
                Value = "15% OFF",
                ExpiryDate = new DateTime()
            };

            return reward;
        }

        public Activity GenerateActivity(Guid contentId)
        {
            var activity = new Activity()
            {
                ActivityId = contentId,
                Name = "Ride in the Mystery.inc truck",
                Description = "Take a guided tour of the towns most " +
                "mysterious attractions in a mystery inc truck",
                Capacity = 125
            };

            return activity;
        }

        public Destination GenerateDestination(Guid contentId)
        {
            var destination = new Destination()
            {
                DestinationId = contentId,
                Name = "Scooby Ville",
                Description = "The scooby themed holiday destination",
                Capacity = 50
            };

            return destination;
        }

        public History GenerateHistory(Guid userId, Guid contentId)
        {
            var history = new History()
            {
                UserId = userId.ToString(),
                ContentId = contentId,
                Date = DateTime.Now
            };

            return history;
        }

        public Restaurant GenerateRestaurant(Guid contentId)
        {
            var restaurant = new Restaurant()
            {
                RestaurantId = contentId,
                Name = "Burger King",
                Description = "Come dine Shaggy and Scooby style at the Burger King!",
                Capacity = 25
            };
            return restaurant;
        }

        public Shortlist GenerateShortlist(Guid userId)
        {
            var shortlist = new Shortlist()
            {
                UserId = userId,
                ListId = Guid.NewGuid(),
                ListName = "Scooby Doo Vacation"
            };

            return shortlist;
        }

        public ShortlistContent GenerateShortlistContent(Guid listId, Guid contentId)
        {
            var shortlistContent = new ShortlistContent()
            {
                ListId = listId,
                ContentId = contentId,
                Number = 1
            };

            return shortlistContent;
        }

        public UserReward GenerateUserReward(Guid userId, Guid rewardId)
        {
            var userReward = new UserReward()
            {
                UserId = userId,
                RewardId = rewardId
            };

            return userReward;
        }
    }
}
