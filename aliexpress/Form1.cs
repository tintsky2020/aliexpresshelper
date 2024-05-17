﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
//using HtmlAgilityPack;
using PuppeteerSharp;
using System.Runtime.InteropServices;
using System.IO;

namespace aliexpress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public PuppeteerSharp.IBrowser browser;
        public int antitime = 40;
        public Boolean isTaskRunning = true;
        private async void Form1_Load(object sender, EventArgs e)
        {
            //"C:\Program Files\Google\Chrome\Application\chrome.exe" --remote-debugging-port=9222
            // 크롬 바로가기에 뒤에 디버깅포트 추가
            var options = new ConnectOptions
            {
                BrowserURL = $"http://127.0.0.1:9222/json/browser"
            };

            browser = await Puppeteer.ConnectAsync(options);

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            isTaskRunning = true;
            // 리치 텍스트 박스에서 주소 목록 추출
            string[] addresses = Regex.Split(richTextBox1.Text, @"\n");

            // 데이터 그리드 초기화
            dataGridView1.Rows.Clear();
            if (textBox1.Text == "")
            {
                textBox1.Text = "1";
            }
            dataGridView1.Columns["originalPrice"].HeaderText = "originalPrice";
            int index = int.Parse(textBox1.Text);
            // 각 주소에 대해 비동기 작업 수행
            await Task.Run(async () =>
            {
                foreach (string address in addresses)
                {
                    if (!isTaskRunning)
                    {
                        break;
                    }
                    //Task.Run(async () =>
                    //{
                    try
                    {
                        // 웹 페이지 HTML 소스 가져오기
                        string html = await GetHtmlAsync(address);

                        if (htmlcheck.Checked)
                        {
                            richTextBox2.Invoke(new Action(() =>
                            {
                                richTextBox2.Text = html;
                            }));
                        }

                        // 최고가, 원래 가격, 할인율 추출
                        (string highestPrice, string originalPrice, string deliveryfee, string discountRate, string deliverydate, string availQuantity) = await ExtractProductInfoAsync(html);

                        // 데이터 그리드에 추가
                        dataGridView1.Invoke(new Action(() =>
                        {

                            dataGridView1.Rows.Add(index++, address, highestPrice, originalPrice, deliveryfee, discountRate, deliverydate, availQuantity);
                            if (dataGridView1.Rows.Count > 0)
                            {
                                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                            }
                        }));

                        if (index % 20 == 0)
                        {
                            antitime = 40;
                            this.Invoke(new Action(() => timer1.Start()));
                            await Task.Delay(40000);
                            //Thread.Sleep(40000);
                            this.Invoke(new Action(() => timer1.Stop()));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    //});
                }
            });
        }




        private async void button2_Click(object sender, EventArgs e)
        {
            isTaskRunning = true;
            // 리치 텍스트 박스에서 주소 목록 추출
            string[] addresses = Regex.Split(richTextBox1.Text, @"\n");

            //richTextBox2.AppendText(addresses[0].ToString());
            // 데이터 그리드 초기화
            dataGridView1.Rows.Clear();
            if (textBox1.Text == "")
            {
                textBox1.Text = "1";
            }

            int index = int.Parse(textBox1.Text);

            dataGridView1.Columns["originalPrice"].HeaderText = "lowprice";

            await Task.Run(async () =>
            {
                // 각 주소에 대해 비동기 작업 수행
                foreach (string address in addresses)
                {
                    if (!isTaskRunning)
                    {
                        break;
                    }

                    try
                    {
                        //richTextBox2.AppendText(address);
                        // 웹 페이지 HTML 소스 가져오기
                        string html = await GetHtmlAsync(address + @"?");

                        if (htmlcheck.Checked) {
                            richTextBox2.Invoke(new Action(() =>
                            {
                                richTextBox2.Text = html;
                            }));
                        }

                        // 최고가, 원래 가격, 할인율 추출
                        (string highestPrice, string lowprice, string deliveryfee, string discountRate) = await ExtractProductInfoAsyncNaver(html);

                        // 데이터 그리드에 추가
                        dataGridView1.Invoke(new Action(() =>
                        {
                            dataGridView1.Rows.Add(index++, address, highestPrice, lowprice, deliveryfee, discountRate);

                            if (dataGridView1.Rows.Count > 0)
                            {
                                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                            }
                        }));

                        if (index % 20 == 0)
                        {
                            antitime = 40;
                            this.Invoke(new Action(() => timer1.Start()));
                            await Task.Delay(40000);
                            //Thread.Sleep(40000);
                            this.Invoke(new Action(() => timer1.Stop()));
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
        }


        private async Task<(string, string, string, string)> ExtractProductInfoAsyncNaver(string html)
        {
            string basePrice = "1";
            string highestPrice = "1";
            string originalPrice = "1";
            string lowPrice = "10000000";
            string discountRate = string.Empty;
            string deliveryfee = string.Empty;

            html=html.Replace("\"","");
            //richTextBox2.Text = "";
            // **여기에 각 웹사이트에 맞는 정보 추출 로직을 구현해야 합니다.**

            // 예시: Aliexpress

            // 최고가
            //MatchCollection highestPriceMatchs = Regex.Matches(html, @"skuActivityAmount\"":{\""currency\"":\""KRW\"",\""formatedAmount\"":\""₩([\s\S]*?)\"",\""value\"":");
            MatchCollection basePriceMatchs = Regex.Matches(html, @"discountedSalePrice:([\s\S]*?),");
            foreach (Match matchb in basePriceMatchs)
            {
                if (matchb.Success)
                {
                    string priceWithoutComma = matchb.Groups[1].Value.Trim().Replace(",", "").Replace("₩", "");

                    if (int.Parse(priceWithoutComma) > int.Parse(basePrice))
                    {
                        basePrice = priceWithoutComma;
                    }
                }
            }

            MatchCollection highestPriceMatchs = Regex.Matches(html, @"price:([\s\S]*?),");
            foreach (Match match in highestPriceMatchs)
            {
                if (match.Success)
                {
                    string priceWithoutComma = match.Groups[1].Value.Trim().Replace(",", "").Replace("₩", "");

                    if (int.Parse(priceWithoutComma) > int.Parse(highestPrice))
                    {
                        highestPrice = priceWithoutComma;
                    }
                    else
                    {
                        lowPrice = priceWithoutComma;
                    }
                    //richTextBox2.AppendText(priceWithoutComma.ToString() + "\r\n");
                }
            }


            /*
            // 원래 가격
            MatchCollection originalPriceMatchs = Regex.Matches(html, @"salePrice\"":([\s\S]*?),");
            foreach (Match match2 in originalPriceMatchs)
            {
                if (match2.Success)
                {
                    string priceWithoutComma = match2.Groups[1].Value.Trim().Replace(",", "").Replace("₩", "");

                    if (int.Parse(priceWithoutComma) > int.Parse(originalPrice))
                    {
                        originalPrice = priceWithoutComma;
                    }
                }
            }
            originalPrice= (int.Parse(originalPrice) + int.Parse(highestPrice)).ToString();
            */
            highestPrice = (int.Parse(basePrice) + int.Parse(highestPrice)).ToString();
            lowPrice = (int.Parse(basePrice) + int.Parse(lowPrice)).ToString();
            // 할인율
            Match discountRateMatch = Regex.Match(html, @"discountedRatio:([\s\S]*?),");
            if (discountRateMatch.Success)
            {
                discountRate = discountRateMatch.Groups[1].Value.Trim();
            }

            Match deliveryMatch = Regex.Match(html, @"baseFee:([\s\S]*?),");
            if (deliveryMatch.Success)
            {
                deliveryfee = deliveryMatch.Groups[1].Value.Trim();
            }

            //return (highestPrice, originalPrice, deliveryfee, discountRate); lowPrice
            return (highestPrice, lowPrice, deliveryfee, discountRate);
        }


        private async Task<(string, string, string, string, string, string)> ExtractProductInfoAsync(string html)
        {
            string highestPrice = "1";
            string originalPrice = "1";
            string discountRate = "1";
            string deliveryfee = "10000000";
            string deliverydate = "1";
            string availQuantity = "";

            html = html.Replace("\\\"", "");
            // **여기에 각 웹사이트에 맞는 정보 추출 로직을 구현해야 합니다.**

            // 예시: Aliexpress

            // 최고가
            MatchCollection highestPriceMatchs = Regex.Matches(html, @"skuActivityAmount:{currency:KRW,formatedAmount:([\s\S]*?),value:");
            foreach (Match match in highestPriceMatchs)
            {
                if (match.Success)
                {
                    string priceWithoutComma = match.Groups[1].Value.Trim().Replace(",", "").Replace("₩", "");

                    if (int.Parse(priceWithoutComma) > int.Parse(highestPrice))
                    {
                        highestPrice = priceWithoutComma;
                    }
                }
            }

            // 원래 가격
            MatchCollection originalPriceMatchs = Regex.Matches(html, @"skuAmount:{currency:KRW,formatedAmount:([\s\S]*?),value");
            foreach (Match match2 in originalPriceMatchs)
            {
                if (match2.Success)
                {
                    string priceWithoutComma = match2.Groups[1].Value.Trim().Replace(",", "").Replace("₩", "");

                    if (int.Parse(priceWithoutComma) > int.Parse(originalPrice))
                    {
                        originalPrice = priceWithoutComma;
                    }
                }
            }

            //재고  skuVal\":{\"availQuantity\":95,
            string pattern = @"skuVal:{availQuantity:([\s\S]*?),";
            MatchCollection match7s = Regex.Matches(html, pattern); //([\s\S]+)\"",
            foreach (Match match5 in match7s)
            {
                if (match5.Success)
                {
                    if (availQuantity != "") { 
                    availQuantity += "," + match5.Groups[1].Value.Trim().Replace(",", "").Replace("₩", "");
                    }
                    else
                    {
                        availQuantity += "'" + match5.Groups[1].Value.Trim().Replace(",", "").Replace("₩", "");
                    }
                }
                else
                {
                    
                }
            }

            // 배송비 shippingFee\":\"74421.0\",\"traceId\":\"21015f9717134923755131465d21d1\",\"trackingAvailable\":true}",
            //string pattern = @"\""shippingFee\"":\""([\s\S]*?)\"",\""traceId\"":\""([\s\S]*?)\"",\""trackingAvailable\"":true";  //@"Amount\"":([\s\S]*?),\"""
            pattern = @"shippingFee:([\S]+),traceId:([\S]+),trackingAvailable:true";
            MatchCollection match5s = Regex.Matches(html, pattern); //([\s\S]+)\"",
            foreach (Match match5 in match5s)
            {
                if (match5.Success)
                {
                    int.TryParse(match5.Groups[1].Value.Replace(".0", ""), out int priceWithoutComma);
                    int.TryParse(deliveryfee, out int deliveryfeei);

                    if (priceWithoutComma < deliveryfeei)
                    {
                        deliveryfee = priceWithoutComma.ToString();
                    }
                }
                else
                {
                    deliveryfee = "false";
                }
            }


            // 할인율
            MatchCollection discountRateMatch = Regex.Matches(html, @"discountTips:([\s\S]*?)%");
            foreach (Match matchd in discountRateMatch)
            {
                if (matchd.Success)
                {
                    int.TryParse(matchd.Groups[1].Value, out int matchdc);
                    int.TryParse(discountRate, out int discountRate2);

                    if (discountRate2 > matchdc)
                    {
                        discountRate = matchd.Groups[1].Value;
                    }
                }
            }

            //배송일
            Match deliverdateminMatch = Regex.Match(html, @"deliveryDayMin:([\s\S]*?),"); //deliveryDate\"":\""([\s\S]*?)\"",");
            if (deliverdateminMatch.Success)
            {
                deliverydate = deliverdateminMatch.Groups[1].Value.Trim() + "~";
            }

            Match deliverdateMatch = Regex.Match(html, @"deliveryDayMax:([\s\S]*?),"); //deliveryDate\"":\""([\s\S]*?)\"",");
            if (deliverdateMatch.Success)
            {
                deliverydate += deliverdateMatch.Groups[1].Value.Trim();
            }



            return (highestPrice, originalPrice, deliveryfee, discountRate, deliverydate, availQuantity);
        }

        private async Task<string> GetHtmlAsync(string address)
        {
            /*
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string html = await client.GetStringAsync(address);
                    //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    //doc.LoadHtml(html);
                    return html;
                    //return doc.DocumentNode.InnerHtml.ToString();
                }
                catch(HttpRequestException e)
                {
                    return e.ToString();
                }
            }
            */

            using (var page = await browser.NewPageAsync())
            {
                page.SetViewportAsync(new PuppeteerSharp.ViewPortOptions { Width = 1280, Height = 1020 });
                await page.GoToAsync(address);

                // 페이지 가져오기
                var html = await page.GetContentAsync();

                // 콘솔에 출력
                return html;
            }
        }

        private async Task<string> GetHtmlAsyncnaver(string address)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string html = await client.GetStringAsync(address);
                    //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    //doc.LoadHtml(html);
                    return html;
                    //return doc.DocumentNode.InnerHtml.ToString();
                }
                catch (HttpRequestException e)
                {
                    return e.ToString();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            richTextBox2.Text = "prevent block remain time : " + (--antitime) + "s\r\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isTaskRunning)
            {
                isTaskRunning = false;
            }

            /*
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
            */
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

        }
    }
}
