using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using HSNHospitalProject.Models;
using HSNHospitalProject.Models.ViewModels;
using Stripe;

namespace HSNHospitalProject.Models
{
    public class DonationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Donation
        public ActionResult Index()
        {
            return View(db.Donations.ToList());
        }

        // GET: Donation/Details/8
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific Donation
            Donation donation = db.Donations.Find(id);

            //Could not find the specific Donation
            if (donation == null)
            {
                return HttpNotFound();
            }

            //return the Donation data 
            return View(donation);
        }

        // GET: Donation/Create
        public ActionResult Create()
        {
            return View();
        }

        //When user submits the form to add a new Donation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DonationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Debug Purpose to see if we are getting the data
                Debug.WriteLine("I'm pulling data of Name: " + model.name + ", Amount: " + model.amount + ", Email: " + model.email
                    + ", Anonymous: " + model.anonymous);
                    //+ ", Card Number: " + model.number + " Card ExpMonth: " + model.expMonth
                    //+ " Card ExpYear: " + model.expYear + " Card CVC: " + model.cvc);

                Donation newDonation = new Donation();
                newDonation.donationName = model.name;
                newDonation.donationAmount = model.amount;
                newDonation.donationEmail = model.email;
                newDonation.donationAnonymous = model.anonymous;

                //This method is when you want the payment done on the Server Side (not safe due to sending card info.)
                /*
                StripeConfiguration.ApiKey = "sk_test_T3BF2ap8TTDpmetCKxF038r400HAf7zrj8";
                var options = new PaymentIntentCreateOptions
                {
                    Amount = model.amount,
                    Currency = "usd",
                    // Verify your integration in this guide by including this parameter
                    Metadata = new Dictionary<string, string>
                    {
                      { "integration_check", "accept_a_payment" },
                    }
                };

                var service = new PaymentIntentService();
                try
                {
                    var paymentIntent = service.Create(options);

                    //Create the Payment Method
                    PaymentMethodCreateOptions payMethodOptions = new PaymentMethodCreateOptions
                    {
                        Type = "card",
                        Card = new PaymentMethodCardCreateOptions
                        {
                            Number = model.number,
                            ExpMonth = model.expMonth,
                            ExpYear = model.expYear,
                            Cvc = model.cvc,
                        },
                        BillingDetails = new BillingDetailsOptions
                        {
                            Name = model.name
                        }
                    };

                    //Confirm the payment we are making
                    PaymentMethodService payMethodService = new PaymentMethodService();
                    PaymentMethod payMethod = payMethodService.Create(payMethodOptions);
                    PaymentIntentConfirmOptions payOptions = new PaymentIntentConfirmOptions
                    {
                        PaymentMethod = payMethod.Id
                    };
                    service = new PaymentIntentService();
                    service.Confirm(
                      paymentIntent.Id,
                      payOptions
                    );

                    //Add the id for the payment in the Stripe API (Unique identifier for the object)
                    newDonation.donationReceiptId = paymentIntent.Id;
                }
                catch (Exception e) {
                    Debug.WriteLine(e.Message);
                    //Something failed, redisplay form
                    return View(model);
                }
                */

                //Add the object to the database
                db.Donations.Add(newDonation);

                //Save the changes in the database
                db.SaveChanges();

                //Must confirm the payment
                return RedirectToAction("CardPayment/" + newDonation.donationId);
                
                //Go back to the list of Donation to see the added Donation (If everything is done in the Server Side)
                //return Redirect("List");
            }

            //Something failed, redisplay form
            return View(model);
        }

        // GET: Donation/CardPayment
        public ActionResult CardPayment(int? id)
        {
            if (id == null)
            {
                //change to redirect to 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific department
            Donation donation = db.Donations.Find(id);

            //Could not find the specific department
            if (donation == null)
            {
                return HttpNotFound();
            }

            StripeConfiguration.ApiKey = "sk_test_T3BF2ap8TTDpmetCKxF038r400HAf7zrj8";
            //Create a Payment Intent from Payment API
            PaymentIntentCreateOptions options = new PaymentIntentCreateOptions
            {
                Amount = donation.donationAmount,
                Currency = "usd",
                // Verify your integration in this guide by including this parameter
                Metadata = new Dictionary<string, string>
                {
                    { "integration_check", "accept_a_payment" },
                }
            };

            PaymentIntentService service = new PaymentIntentService();
            PaymentIntent paymentIntent = service.Create(options);
            ViewData["id"] = id;
            ViewData["clientSecret"] = paymentIntent.ClientSecret;
            ViewData["clientName"] = db.Donations.Find(id).donationName;

            //Add the id for the payment in the Stripe API (Unique identifier for the object)
            db.Donations.Find(id).donationReceiptId = paymentIntent.Id;

            //Save the changes in the database
            db.SaveChanges();

            return View();
        }

        //When user submits the form of the card info. and was successful
        [HttpPost]
        public ActionResult CardPayment() {
            //Go back to the list of Donation to see the added Donation
            return Redirect("Index");
        }

        //When user wants to cancel the payment
        public ActionResult CancelPayment(int id) {
            StripeConfiguration.ApiKey = "sk_test_T3BF2ap8TTDpmetCKxF038r400HAf7zrj8";

            Donation removedDonation = db.Donations.Find(id);

            //Cancel the Payment
            var service = new PaymentIntentService();
            service.Cancel(removedDonation.donationReceiptId);

            //Delete the Donation from the table
            db.Donations.Remove(removedDonation);

            //Save the changes on the database
            db.SaveChanges();

            //Go back to the list of Donation
            return RedirectToAction("Index");
        }     
            
        // GET: Donation/Delete/8
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific Donation
            Donation donation = db.Donations.Find(id);

            //Could not find the specific Donation
            if (donation == null)
            {
                return HttpNotFound();
            }

            //return the Donation data 
            return View(donation);
        }

        // POST: Donation/Delete/8
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Debug Purpose to see if we are getting the id
            Debug.WriteLine("I'm pulling data of " + id.ToString());

            //Get the specific Donation
            Donation donation = db.Donations.Find(id);

            //Refund the donated amount back to the user
            //Code Referenced from: https://stripe.com/docs/refunds
            StripeConfiguration.ApiKey = "sk_test_T3BF2ap8TTDpmetCKxF038r400HAf7zrj8";
            var refunds = new RefundService();
            var refundOptions = new RefundCreateOptions
            {
                PaymentIntent = donation.donationReceiptId,
                Amount = donation.donationAmount
            };
            var refund = refunds.Create(refundOptions);

            //Delete that specific Donation from the database
            db.Donations.Remove(donation);

            //Save the changes on the database
            db.SaveChanges();

            //Go back to the list of Donation to see the removed Donation
            return RedirectToAction("Index");
        }

    }
}