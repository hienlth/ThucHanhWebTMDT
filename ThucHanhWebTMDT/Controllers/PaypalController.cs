using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucHanhWebTMDT.Helper;

namespace ThucHanhWebTMDT.Controllers
{
    public class PaypalController : Controller
    {
        private Payment payment;
        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithPaypal()
        {

            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";

                    //Generate paymentID
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //Guid.NewGuid();

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);


                    //link return
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);

                }
                else
                {
                    // Executing a payment
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId,
                    Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");//View hiển thị thông báo thanh toán thất bại
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }


            return View("Success");
        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        public static double TongTien = 0;
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var itemList = new ItemList() { items = new List<Item>() };
            //Các giá trị bao gồm danh sách sản phẩm, thông tin đơn hàng
            //Sẽ được thay đổi bằng hành vi thao tác mua hàng trên website
            Random rd = new Random();
            int soMatHang = rd.Next(1, 5);
            //thêm ds sản phẩm
            for(int i = 0; i < soMatHang; i++)
            {
                Item item = new Item()
                {
                    //Thông tin đơn hàng
                    name = "Samsung Galaxy J" + rd.Next(1, 15).ToString(),
                    currency = "USD",
                    price = rd.Next(10, 100).ToString(),//giá
                    quantity = rd.Next(1, 10).ToString(),//số lượng
                    sku = "sku"
                };
                TongTien += double.Parse(item.price) * double.Parse(item.quantity);
                itemList.items.Add(item);
            }
            

            //Hình thức thanh toán qua paypal
            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            //các thông tin trong đơn hàng
            var details = new Details()
            {
                tax = "1",//thuế
                shipping = "1",//phí vận chuyển
                subtotal = TongTien.ToString()//giá
            };

            //Đơn vị tiền tệ và tổng đơn hàng cần thanh toán
            var amount = new Amount()
            {
                currency = "USD",
                total = (TongTien + 2).ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };


            var transactionList = new List<Transaction>();
            //Tất cả thông tin thanh toán cần đưa vào transaction
            transactionList.Add(new Transaction()
            {
                description = "Thanh toán đơn hàng có mã 12345",
                invoice_number = "MaHD:" + Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }
    }
}