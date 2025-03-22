using BoDi;
using Newtonsoft.Json;
using NUnit.Framework;
using PageLayers.API;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using TestLayers.Hooks;

namespace TestLayers.StepDefinitions
{
    
    [Binding]
    public class BookingApiTestsStepDefinitions
    {

        private TokenGenerate _tokenGenerate;
        private string _token;
        private RestClient _restclient;
        private RestResponse _response;
        private RestRequest _request;



        public BookingApiTestsStepDefinitions(IObjectContainer objectContainer)
        {
            _restclient = objectContainer.Resolve<RestClient>();
            var hooks = objectContainer.Resolve<Hooks1>();
            _token = hooks.GetToken();
        }



        [Given(@"I have the booking details")]
        public void GivenIHaveTheBookingDetails()
        {
            _request = new RestRequest("/booking", Method.Post);
            _request.AddHeader("Accept", "*/*");
            _request.AddHeader("Content-Type", "application/json");
        }

        [When(@"I create a new booking")]
        public void WhenICreateANewBooking()
        {
            var booking = new BookingModel
            {
                Firstname = "Alex",
                Lastname = "Hales",
                Totalprice = 7521,
                Depositpaid = true,
                Bookingdates = new BookingDates
                {
                    Checkin = "2019-11-09",
                    Checkout = "2023-05-07"
                },
                Additionalneeds = "BreakFast"
            };

            _request.AddJsonBody(booking);
            _response = _restclient.Execute(_request);

            var bookingResponse = JsonConvert.DeserializeObject<BookingResponse>(_response.Content);

        }

        [Then(@"the booking should be created successfully")]
        public void ThenTheBookingShouldBeCreatedSuccessfully()
        {
           
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Console.WriteLine(_response.Content);

        }

        [Given(@"I have an existing booking ID")]
        public void GivenIHaveAnExistingBookingID()
        {

            _request = new RestRequest("/booking/1770", Method.Get);
            _request.AddHeader("Accept", "*/*");
            _request.AddHeader("Content-Type", "application/json");

        }

        [When(@"I retrieve the booking details")]
        public void WhenIRetrieveTheBookingDetails()
        {
            _response = _restclient.Execute(_request);
        }

        [Then(@"the booking details should be returned successfully")]
        public void ThenTheBookingDetailsShouldBeReturnedSuccessfully()
        {
            var bookingDetails = JsonConvert.DeserializeObject<BookingModel>(_response.Content);

            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(bookingDetails, Is.Not.Null);
            Assert.That(bookingDetails.Firstname, Is.Not.Empty);
            Assert.That(bookingDetails.Lastname, Is.Not.Empty);
            Assert.That(bookingDetails.Totalprice, Is.GreaterThan(0));

        }

        [Given(@"I have updated booking details")]
        public void GivenIHaveUpdatedBookingDetails()
        {
            _request = new RestRequest("/booking/1770", Method.Put);
            _request.AddHeader("Accept", "*/*");
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Cookie", $"token={_token}");
        }

        [When(@"I update the booking")]
        public void WhenIUpdateTheBooking()
        {

            var booking = new BookingModel
            {
                Firstname = "Sumanta",
                Lastname = "Swain",
                Totalprice = 6621,
                Depositpaid = true,
                Bookingdates = new BookingDates
                {
                    Checkin = "2022-11-09",
                    Checkout = "2024-05-07"
                },
                Additionalneeds = "Dinner"

            };
            _request.AddJsonBody(booking);
            _response = _restclient.Execute(_request);

        }

        [Then(@"the booking should be updated successfully")]
        public void ThenTheBookingShouldBeUpdatedSuccessfully()
        {

            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Given(@"I have updated booking details for a specific field")]
        public void GivenIHaveUpdatedBookingDetailsForASpecificField()
        {
            _request = new RestRequest("/booking/1770", Method.Patch);
            _request.AddHeader("Accept", "*/*");
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Cookie", $"token={_token}");

        }

        [When(@"I partially update the booking")]
        public void WhenIPartiallyUpdateTheBooking()
        {
            var booking = new BookingModel
            {
                Firstname = "Siri",
                Lastname = "Joe",
                Bookingdates = new BookingDates
                {
                    Checkin = "2019-11-09",
                    Checkout = "2023-05-07"
                },

            };
            _request.AddJsonBody(booking);
            _response = _restclient.Execute(_request);
        }

        [Then(@"the booking should be updated successfully with the new details")]
        public void ThenTheBookingShouldBeUpdatedSuccessfullyWithTheNewDetails()
        {

            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [When(@"I delete the booking")]
        public void WhenIDeleteTheBooking()
        {
            _request = new RestRequest("booking/1770", Method.Delete);
            _request.AddHeader("Accept", "*/*");
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Cookie", $"token={_token}");
            _response = _restclient.Execute(_request);

        }

        [Then(@"the booking should be deleted successfully")]
        public void ThenTheBookingShouldBeDeletedSuccessfully()
        {

            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(_response.Content, Is.EqualTo("Created"));

        }
    }
}
