﻿using AutoMapper;
using webApi.Controllers;
using webApi.DataTransferObjects;
using webApi.Helpers;
using webApi.Models;
using webApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using webApi.DataTransferObjects.RestaurantDTO;
using webApi.Exceptions;
using webApi.DataTransferObjects.AddressDTO;
using webApi.DataTransferObjects.SectionDTO;
using webApi.DataTransferObjects.DishDTO;

namespace webApiTest
{
    public class RestaurantUnitTest
    {
        RestaurantController restaurantController;
        IO2_RestaurantsContext context;
        bool seeded = false;

        private void Seed(IO2_RestaurantsContext context)
        {
            context.Addresses.AddRange(
                new Address() { Id = 1, City = "Warsaw", Street = "Aleje Jerozolimskie", PostCode = "96-500" },
                new Address() { Id = 2, City = "London", Street = "Aleje Jerozolimskie", PostCode = "96-500" },
                new Address() { Id = 3, City = "New York", Street = "Aleje Jerozolimskie", PostCode = "96-500" },
                new Address() { Id = 4, City = "Kijev", Street = "Aleje Jerozolimskie", PostCode = "96-500" },
                new Address() { Id = 5, City = "Alabama", Street = "Aleje Jerozolimskie", PostCode = "96-500" },
                new Address() { Id = 6, City = "Jerozolima", Street = "Aleje Jerozolimskie", PostCode = "96-500" },
                new Address() { Id = 7, City = "Kracow", Street = "Aleje Jerozolimskie", PostCode = "96-500" },
                new Address() { Id = 8, City = "Sosnowiec", Street = "Aleje Jerozolimskie", PostCode = "96-500" }
                );

            context.Restaurants.AddRange(
                new Restaurant() { Id = 1, Name = "Kasza Jaglana Restauracja", ContactInformation = "kasza@jaglak.pl", Rating = 4.1m, State = 0, Owing = 110.50m, DateOfJoining = new DateTime(2020, 3, 20, 11, 59, 59), AggregatePayment = 1000.49m, AddressId = 4 },
                new Restaurant() { Id = 2, Name = "Restauracja Magdy Gessler", ContactInformation = "mg@tvn.pl", Rating = 4.1m, State = 1, Owing = 110.50m, DateOfJoining = new DateTime(2020, 3, 20, 11, 59, 59), AggregatePayment = 1000.49m, AddressId = 5 },
                new Restaurant() { Id = 3, Name = "Top Restauracja", ContactInformation = "rest@top.pl", Rating = 4.1m, State = 2, Owing = 110.50m, DateOfJoining = new DateTime(2020, 3, 20, 11, 59, 59), AggregatePayment = 1000.49m, AddressId = 6 }
                );

            context.Sections.AddRange(
                new Section() { Id = 1, Name = "Kasze", RestaurantId = 1 },
                new Section() { Id = 2, Name = "Napoje", RestaurantId = 1 },
                new Section() { Id = 3, Name = "Zupy", RestaurantId = 2 },
                new Section() { Id = 4, Name = "Pierogi", RestaurantId = 2 },
                new Section() { Id = 5, Name = "Napoje gazowane", RestaurantId = 3 },
                new Section() { Id = 6, Name = "Napoje niegazowane", RestaurantId = 3 }
                );

            context.Dishes.AddRange(
                new Dish() { Id = 1, Name = "Kasza jaglana", Description = "Najlepsza kasza na świecie", Price = 24.59m, SectionId = 1 },
                new Dish() { Id = 2, Name = "Kaszanka", Description = "Idealna na grilla", Price = 24.59m, SectionId = 1 },
                new Dish() { Id = 3, Name = "Woda", Description = "Naturalne orzeźwienie", Price = 24.59m, SectionId = 1 },
                new Dish() { Id = 4, Name = "Pomidorowa", Description = "Z pomidorów z nad Bałtyku", Price = 24.59m, SectionId = 1 },
                new Dish() { Id = 5, Name = "Z mięsem", Description = "Mięęęęęso", Price = 24.59m, SectionId = 1 },
                new Dish() { Id = 6, Name = "Ze szpinakiem", Description = "Idealne dla wegetarian", Price = 24.59m, SectionId = 1 },
                new Dish() { Id = 7, Name = "Pepsi", Description = "Nie cola", Price = 24.59m, SectionId = 1 }
                );

            context.Users.AddRange(
                new User() { Id = 1, Name = "Micheal", Surname = "Jackson", Email = "abc@s1.pl", IsRestaurateur = true, IsAdministrator = false, CreationDate = new DateTime(2020, 3, 20, 11, 59, 59), PasswordHash = "abcabcabc", AddressId = 1, RestaurantId = 1 },
                new User() { Id = 2, Name = "Elisabeth", Surname = "Smith", Email = "abc@s2.pl", IsRestaurateur = false, IsAdministrator = true, CreationDate = new DateTime(2020, 3, 20, 11, 59, 59), PasswordHash = "abcabcabc", AddressId = 2, RestaurantId = null },
                new User() { Id = 3, Name = "Daniel", Surname = "Craig", Email = "abc@s3.pl", IsRestaurateur = false, IsAdministrator = false, CreationDate = new DateTime(2020, 3, 20, 11, 59, 59), PasswordHash = "abcabcabc", AddressId = 3, RestaurantId = null }
                );

            context.DiscountCodes.AddRange(
                new DiscountCode() { Id = 1, Percent = 10, Code = "JAGLAK", DateFrom = new DateTime(2020, 3, 20, 11, 59, 59), DateTo = new DateTime(2020, 3, 20, 11, 59, 59), RestaurantId = 1 },
                new DiscountCode() { Id = 2, Percent = 20, Code = "JAGLAK-CODE", DateFrom = new DateTime(2020, 3, 20, 11, 59, 59), DateTo = new DateTime(2020, 3, 20, 11, 59, 59), RestaurantId = 1 }
                );

            context.Orders.AddRange(
                new Order() { Id = 1, PaymentMethod = 0, State = 2, Date = new DateTime(2020, 3, 20, 11, 59, 59), AddressId = 7, DiscountCodeId = 1, CustomerId = 3, RestaurantId = 1, EmployeeId = null },
                new Order() { Id = 2, PaymentMethod = 1, State = 3, Date = new DateTime(2020, 3, 20, 11, 59, 59), AddressId = 8, DiscountCodeId = null, CustomerId = 3, RestaurantId = 1, EmployeeId = null }
                );

            context.Complaints.Add(
                new Complaint() { Id = 1, Content = "Jedzenie za zimne", Response = "Przepraszamy za niedogodność", Open = false, CustomerId = 3, OrderId = 1 }
                );

            context.Reviews.Add(
                new Review() { Id = 1, Content = "Jedzenie z restauracji jest zimne", Rating = 1.0m, CustomerId = 3, RestaurantId = 1 }
                );

            context.OrderDishes.AddRange(
                new OrderDish() { Id = 1, DishId = 1, OrderId = 2 },
                new OrderDish() { Id = 2, DishId = 1, OrderId = 1 },
                new OrderDish() { Id = 3, DishId = 2, OrderId = 1 }
                );

            context.SaveChanges();
        }

        [SetUp]
        public void Setup()
        {
            if (!seeded)
            {
                var options = new DbContextOptionsBuilder<IO2_RestaurantsContext>()
                    .UseInMemoryDatabase(databaseName: "IO2_Restaurants")
                    .Options;
                context = new IO2_RestaurantsContext(options);
                Seed(context);

                var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
                var mapper = new Mapper(config);
                var service = new RestaurantService(context, mapper);
                restaurantController = new RestaurantController(service);


                restaurantController.ControllerContext.HttpContext = new DefaultHttpContext();
                restaurantController.ControllerContext.HttpContext.Items["Restaurant"] = context.Restaurants.FirstOrDefault(el => el.Id == 1);
                restaurantController.ControllerContext.HttpContext.Request.Headers["X-Forwarded-For"] = "127.0.0.1";

                seeded = true;
            }
        }

        [Test]
        public void GetRestaurantTest()
        {
            var response = restaurantController.GetRestaurant(1);
            var result = response.Result as ObjectResult;
            var restaurant = result.Value as RestaurantDTO;

            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(1, restaurant.Id);

            Assert.Catch<NotFoundException>(() =>
            {
                response = restaurantController.GetRestaurant(1000);
            });
        }

        [Test]
        public void CreateRestaurantTest()
        {
            NewRestaurant newRestaurant = new NewRestaurant()
            {
                Address = new AddressDTO { City = "Warsaw", Street = "Andersena", PostCode = "01-003" },
                ContactInformation = "new@rest.pl",
                Name = "New Restaurant"
            };

            var response = restaurantController.CreateRestaurant(newRestaurant);
            var result = response as ObjectResult;

            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void DeleteRestaurantTest()
        {
            var rest = context.Restaurants.FirstOrDefault(r => r.Name == "New Restaurant");

            var response = restaurantController.GetRestaurant(rest.Id);
            var result = response.Result as ObjectResult;
            var restaurant = result.Value as RestaurantDTO;

            Assert.AreEqual(200, result.StatusCode);

            var response2 = restaurantController.DeleteRestaurant(rest.Id);
            var result2 = response2 as ObjectResult;

            Assert.AreEqual(200, result.StatusCode);

            Assert.Catch<NotFoundException>(() =>
            {
                restaurantController.GetRestaurant(rest.Id);
            });

            Assert.Catch<NotFoundException>(() =>
            {
                restaurantController.DeleteRestaurant(1000);
            });
        }

        [Test]
        public void GetSection()
        {
            var response = restaurantController.GetSectionByRestaurantsId(1);
            var result = response.Result as ObjectResult;
            var sections = result.Value as List<SectionDTO>;

            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(2, sections.Count);

            Assert.Catch<NotFoundException>(() =>
            {
                response = restaurantController.GetSectionByRestaurantsId(1000);
            });
        }

        [Test]
        public void CreateSectionTest()
        {
            var response = restaurantController.CreateSection(1, "Nowa sekcja");
            var result = response as ObjectResult;

            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void DeleteSectionTest()
        {

        }
    }
}