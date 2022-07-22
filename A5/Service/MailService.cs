using MimeKit;
using A5.Models;
using A5.Service.Interfaces;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using MailKit.Security;

namespace A5.Service
{

    public class MailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            var filepath = $"template.txt";
            builder.TextBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public static MailRequest AwardMail(string RecieverEmail, string AwardTitle, string Subject, int AwardStatusId)
        {
            var mail = new MailRequest();
            mail.ToEmail = RecieverEmail;
            mail.Subject = Subject;
            switch (AwardStatusId)
            {
                // case 1: mail.Body=StringWriter.;
                //         return mail;
                case 4:
                    mail.Body = $"Hello Aspirian,\n\nGreetings,\n\n Your Article - \"{AwardTitle}\" have been published successfully.\n\n Thanks and Regards,\nAspireOverflow.\n";
                    return mail;
                default: return mail;
            }
        }





        public async Task PublishedAsync(Award? awardee)
        {

            string text1 =
                $@"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>
<html data-editor-version='2' class='sg-campaigns' xmlns='http://www.w3.org/1999/xhtml'>
    <head>
      <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
      <meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>
      <!--[if !mso]><!-->
      <meta http-equiv='X-UA-Compatible' content='IE=Edge'>
      <!--<![endif]-->
      <!--[if (gte mso 9)|(IE)]>
      <xml>
        <o:OfficeDocumentSettings>
          <o:AllowPNG/>
          <o:PixelsPerInch>96</o:PixelsPerInch>
        </o:OfficeDocumentSettings>
      </xml>
      <![endif]-->
      <!--[if (gte mso 9)|(IE)]>
  
      <!--user entered Head Start--><!--End Head user entered-->
    </head>
    <body>
      <center class='wrapper' data-link-color='#1188E6' data-body-style='font-size:14px; font-family:arial,helvetica,sans-serif; color:#000000; background-color:#e0d5b3;'>
        <div class='webkit'>
          <table cellpadding='0' cellspacing='0' border='0' width='100%' class='wrapper' bgcolor='#e0d5b3'>
            <tr>
              <td valign='top' bgcolor='#e0d5b3' width='100%'>
                <table width='100%' role='content-container' class='outer' align='center' cellpadding='0' cellspacing='0' border='0'>
                  <tr>
                    <td width='100%'>
                      <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                        <tr>
                          <td>
                            <!--[if mso]>
    <center>
    <table><tr><td width='700'>
  <![endif]-->
                                    <table width='100%' cellpadding='0' cellspacing='0' border='0' style='width:100%; max-width:700px;' align='center'>
                                      <tr>
                                        <td role='modules-container' style='padding:0px 0px 0px 0px; color:#000000; text-align:left;' bgcolor='#E0D5B3' width='100%' align='left'><table class='module preheader preheader-hide' role='module' data-type='preheader' border='0' cellpadding='0' cellspacing='0' width='100%' style='display: none !important; mso-hide: all; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;'>
    <tr>
      <td role='module-content'>
        <p>Congratulations!</p>
      </td>
    </tr>
  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='0fd7a287-e017-4d1b-87f0-bf7e517f6406' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:15px 10px 20px 0px; line-height:16px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div><div style='font-family: inherit; text-align: right'><span style='font-family: &quot;times new roman&quot;, times, serif; font-size: 12px'></span></div>
<div style='font-family: inherit; text-align: right'><span style='font-family: &quot;times new roman&quot;, times, serif; color: #c29a1f; font-size: 12px'></span><span style='font-family: &quot;times new roman&quot;, times, serif; font-size: 12px'></span></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' role='module' data-type='columns' style='padding:0px 0px 0px 0px;' bgcolor='' data-distribution='1'>
    <tbody>
      <tr role='module-content'>
        <td height='100%' valign='top'><table width='700' style='width:700px; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor='' class='column column-0'>
      <tbody>
        <tr>
          <td style='padding:0px;margin:0px;border-spacing:0;'><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='eb8f13b4-163e-4e06-b97a-299f04852feb'>
    <tbody>
      <tr>
        <td style='font-size:6px; line-height:10px; padding:0px 0px 0px 0px;' valign='top' align='center'>
          <img class='max-width' border='0' style='display:block; color:#000000; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px; max-width:100% !important; width:100%; height:auto !important;' width='700' alt='' data-proportionally-constrained='true' data-responsive='true' src='http://cdn.mcauto-images-production.sendgrid.net/b841a9c8d79766a3/52d792c7-b069-4be5-9e9d-5570beca9466/1748x1240.png'>
        </td>
      </tr>
    </tbody>
  </table></td>
        </tr>
      </tbody>
    </table></td>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' role='module' data-type='columns' style='padding:20px 0px 0px 0px;' bgcolor='#F4F0E4' data-distribution='1'>
    <tbody>
      <tr role='module-content'>
        <td height='100%' valign='top'><table width='680' style='width:680px; border-spacing:0; border-collapse:collapse; margin:0px 10px 0px 10px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor='' class='column column-0'>
      <tbody>
        <tr>
          <td style='padding:0px;margin:0px;border-spacing:0;'><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='9f929b4b-e5da-486f-bdda-59da318ed1ea.1' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:0px 60px 0px 60px; line-height:28px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div><div style='font-family: inherit; text-align: center'><span style='font-family: &quot;times new roman&quot;, times, serif; color: #403101; font-size: 20px'>Hi {awardee?.Awardee?.FirstName}, We are Congratulating you on receiving {awardee?.AwardType?.AwardName}</em></span></div>
<div style='font-family: inherit; text-align: center'><span style='font-family: &quot;times new roman&quot;, times, serif; color: #403101; font-size: 20px'><em>You have won a coupon  </em></span><span style='font-family: &quot;times new roman&quot;, times, serif; color: #c29a1f; font-size: 20px'><strong>{awardee?.CouponCode}</strong></span><span style='font-family: &quot;times new roman&quot;, times, serif; color: #403101; font-size: 20px'><em>!</em></span></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' class='module' data-role='module-button' data-type='button' role='module' style='table-layout:fixed;' width='100%' data-muid='183cbc8c-7906-496a-8e51-d93e0f2a294a'>
      <tbody>
        <tr>
          <td align='center' bgcolor='' class='outer-td' style='padding:25px 0px 30px 0px;'>
            <table border='0' cellpadding='0' cellspacing='0' class='wrapper-mobile' style='text-align:center;'>
              <tbody>
                <tr>
                <td align='center' bgcolor='#5a8dbb' class='inner-td' style='border-radius:6px; font-size:16px; text-align:center; background-color:inherit;'><a href='' style='background-color:#5a8dbb; border:0px solid #333333; border-color:#333333; border-radius:0px; border-width:0px; color:#F4F0E4; display:inline-block; font-size:16px; font-weight:normal; letter-spacing:2px; line-height:normal; padding:20px 60px 20px 60px; text-align:center; text-decoration:none; border-style:solid; font-family:times new roman,times,serif;' target='_blank'>Click here</a></td>
                </tr>
              </tbody>
            </table>
          </td>
        </tr>
      </tbody>
    </table></td>
        </tr>
      </tbody>
    </table></td>
      </tr>
    </tbody>
  </table>
    </body>
  </html>";

            await File.WriteAllTextAsync(@"C:\Users\aakaash.mani\Desktop\Published.html", text1);
        }


        public async Task RequesterAsync(Award? awardee)
        {

            string text2 =
                $@"    <body>
      <center class='wrapper' data-link-color='#1188E6' data-body-style='font-size:14px; font-family:inherit; color:#000000; background-color:#FFFFFF;'>
        <div class='webkit'>
          <table cellpadding='0' cellspacing='0' border='0' width='100%' class='wrapper' bgcolor='#FFFFFF'>
            <tr>
              <td valign='top' bgcolor='#FFFFFF' width='100%'>
                <table width='100%' role='content-container' class='outer' align='center' cellpadding='0' cellspacing='0' border='0'>
                  <tr>
                    <td width='100%'>
                      <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                        <tr>
                          <td>
                            <!--[if mso]>
    <center>
    <table><tr><td width='600'>
  <![endif]-->
                                    <table width='100%' cellpadding='0' cellspacing='0' border='0' style='width:100%; max-width:600px;' align='center'>
                                      <tr>
                                        <td role='modules-container' style='padding:0px 0px 0px 0px; color:#000000; text-align:left;' bgcolor='#FFFFFF' width='100%' align='left'><table class='module preheader preheader-hide' role='module' data-type='preheader' border='0' cellpadding='0' cellspacing='0' width='100%' style='display: none !important; mso-hide: all; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;'>
    <tr>
      <td role='module-content'>
        <p></p>
      </td>
    </tr>
  </table><table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' role='module' data-type='columns' style='padding:30px 20px 30px 20px;' bgcolor='#f6f6f6' data-distribution='1'>
    <tbody>
      <tr role='module-content'>
        <td height='100%' valign='top'><table width='540' style='width:540px; border-spacing:0; border-collapse:collapse; margin:0px 10px 0px 10px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor='' class='column column-0'>
      <tbody>
        <tr>
          <td style='padding:0px;margin:0px;border-spacing:0;'><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='72aac1ba-9036-4a77-b9d5-9a60d9b05cba'>
    <tbody>
      <tr>
        <td style='font-size:6px; line-height:10px; padding:0px 0px 0px 0px;' valign='top' align='center'>
          <img class='max-width' border='0' style='display:block; color:#000000; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px;' width='29' alt='' data-proportionally-constrained='true' data-responsive='false' src='http://cdn.mcauto-images-production.sendgrid.net/b841a9c8d79766a3/018338dc-a1ab-48e5-a1ca-7ae7ab26e24b/490x490.jpg' height='27'>
        </td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='331cde94-eb45-45dc-8852-b7dbeb9101d7'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 20px 0px;' role='module-content' bgcolor=''>
        </td>
      </tr>
    </tbody>
  </table><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='d8508015-a2cb-488c-9877-d46adf313282'>
    <tbody>
      <tr>
        
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='27716fe9-ee64-4a64-94f9-a4f28bc172a0'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 30px 0px;' role='module-content' bgcolor=''>
        </td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='948e3f3f-5214-4721-a90e-625a47b1c957' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:50px 30px 18px 30px; line-height:36px; text-align:inherit; background-color:#ffffff;' height='100%' valign='top' bgcolor='#ffffff' role='module-content'><div><div style='font-family: inherit; text-align: center'><span style='font-size: 43px'>Hey {awardee?.Awardee?.ReportingPerson?.ReportingPerson?.FirstName}, you have received a request from {awardee?.Awardee?.ReportingPerson?.FirstName}  </span></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='a10dcb57-ad22-4f4d-b765-1d427dfddb4e' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:18px 30px 18px 30px; line-height:22px; text-align:inherit; background-color:#ffffff;' height='100%' valign='top' bgcolor='#ffffff' role='module-content'><div><div style='font-family: inherit; text-align: center'><span style='font-size: 18px'>Visit our site to </span><span style='color: #000000; font-size: 18px; font-family: arial, helvetica, sans-serif'>Get the Detailed information.</span><span style='font-size: 18px'>.</span></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffbe00; font-size: 18px'><strong>Thank you!</strong></span></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='7770fdab-634a-4f62-a277-1c66b2646d8d'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 20px 0px;' role='module-content' bgcolor='#ffffff'>
        </td>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' class='module' data-role='module-button' data-type='button' role='module' style='table-layout:fixed;' width='100%' data-muid='d050540f-4672-4f31-80d9-b395dc08abe1'>
      <tbody>
        <tr>
          <td align='center' bgcolor='#ffffff' class='outer-td' style='padding:0px 0px 0px 0px; background-color:#ffffff;'>
            <table border='0' cellpadding='0' cellspacing='0' class='wrapper-mobile' style='text-align:center;'>
              <tbody>
                <tr>
                <td align='center' bgcolor='#ffbe00' class='inner-td' style='border-radius:6px; font-size:16px; text-align:center; background-color:inherit;'>
                  <a href='' style='background-color:#ffbe00; border:1px solid #ffbe00; border-color:#ffbe00; border-radius:0px; border-width:1px; color:#000000; display:inline-block; font-size:14px; font-weight:normal; letter-spacing:0px; line-height:normal; padding:12px 40px 12px 40px; text-align:center; text-decoration:none; border-style:solid; font-family:inherit;' target='_blank'>Click to Visit</a>
                </td>
                </tr>
              </tbody>
            </table>
          </td>
        </tr>
      </tbody>
    </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='7770fdab-634a-4f62-a277-1c66b2646d8d.1'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 50px 0px;' role='module-content' bgcolor='#ffffff'>
        </td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='a265ebb9-ab9c-43e8-9009-54d6151b1600' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:50px 30px 50px 30px; line-height:22px; text-align:inherit; background-color:#6e6e6e;' height='100%' valign='top' bgcolor='#6e6e6e' role='module-content'><div><div style='font-family: inherit; text-align: center'><span style='color: #ffbe00; font-size: 18px'><strong>Here’s what we provide:</strong></span></div>
<div style='font-family: inherit; text-align: center'><br></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffffff; font-size: 18px'>1. Gain access to give a appreciation for the hardworking ones.</span></div>
<div style='font-family: inherit; text-align: center'><br></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffffff; font-size: 18px'>2. Make yourself to comment on your proud ones.</span></div>
<div style='font-family: inherit; text-align: center'><br></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffffff; font-size: 18px'>3. Get appreciated by all.</span></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffbe00; font-size: 18px'><strong>+ much more!</strong></span></div>
<div style='font-family: inherit; text-align: center'><br></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffffff; font-size: 18px'>Visit our site for more fun.</span></div>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' class='module' data-role='module-button' data-type='button' role='module' style='table-layout:fixed;' width='100%' data-muid='d050540f-4672-4f31-80d9-b395dc08abe1.1'>

    </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='c37cc5b7-79f4-4ac8-b825-9645974c984e'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 30px 0px;' role='module-content' bgcolor='6E6E6E'>
        </td>
      </tr>
    </tbody>
  </table></td>
        </tr>
      </tbody>
    </table></td>
      </tr>
    </tbody>
  </table>
    </table></td>
                                      </tr>
                                    </table>
                                    <!--[if mso]>
                                  </td>
                                </tr>
                              </table>
                            </center>
                            <![endif]-->
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
      </center>
    </body>
  </html>
";

            await File.WriteAllTextAsync(@"C:\Users\aakaash.mani\Desktop\Request_Raised.html", text2);

        }


        public async Task RejectedAsync(Award awardee)
        {

            string text3 =
                $@"    <body>
      <center class='wrapper' data-link-color='#1188E6' data-body-style='font-size:14px; font-family:inherit; color:#000000; background-color:#FFFFFF;'>
        <div class='webkit'>
          <table cellpadding='0' cellspacing='0' border='0' width='100%' class='wrapper' bgcolor='#FFFFFF'>
            <tr>
              <td valign='top' bgcolor='#FFFFFF' width='100%'>
                <table width='100%' role='content-container' class='outer' align='center' cellpadding='0' cellspacing='0' border='0'>
                  <tr>
                    <td width='100%'>
                      <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                        <tr>
                          <td>
                            <!--[if mso]>
    <center>
    <table><tr><td width='600'>
  <![endif]-->
                                    <table width='100%' cellpadding='0' cellspacing='0' border='0' style='width:100%; max-width:600px;' align='center'>
                                      <tr>
                                        <td role='modules-container' style='padding:0px 0px 0px 0px; color:#000000; text-align:left;' bgcolor='#FFFFFF' width='100%' align='left'><table class='module preheader preheader-hide' role='module' data-type='preheader' border='0' cellpadding='0' cellspacing='0' width='100%' style='display: none !important; mso-hide: all; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;'>
    <tr>
      <td role='module-content'>
        <p></p>
      </td>
    </tr>
  </table><table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' role='module' data-type='columns' style='padding:30px 20px 30px 20px;' bgcolor='#f6f6f6' data-distribution='1'>
    <tbody>
      <tr role='module-content'>
        <td height='100%' valign='top'><table width='540' style='width:540px; border-spacing:0; border-collapse:collapse; margin:0px 10px 0px 10px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor='' class='column column-0'>
      <tbody>
        <tr>
          <td style='padding:0px;margin:0px;border-spacing:0;'><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='72aac1ba-9036-4a77-b9d5-9a60d9b05cba'>
    <tbody>
      <tr>
        <td style='font-size:6px; line-height:10px; padding:0px 0px 0px 0px;' valign='top' align='center'>
          <img class='max-width' border='0' style='display:block; color:#000000; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px;' width='29' alt='' data-proportionally-constrained='true' data-responsive='false' src='http://cdn.mcauto-images-production.sendgrid.net/b841a9c8d79766a3/018338dc-a1ab-48e5-a1ca-7ae7ab26e24b/490x490.jpg' height='27'>
        </td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='331cde94-eb45-45dc-8852-b7dbeb9101d7'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 20px 0px;' role='module-content' bgcolor=''>
        </td>
      </tr>
    </tbody>
  </table><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='d8508015-a2cb-488c-9877-d46adf313282'>
    <tbody>
      <tr>
        
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='27716fe9-ee64-4a64-94f9-a4f28bc172a0'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 30px 0px;' role='module-content' bgcolor=''>
        </td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='948e3f3f-5214-4721-a90e-625a47b1c957' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:50px 30px 18px 30px; line-height:36px; text-align:inherit; background-color:#ffffff;' height='100%' valign='top' bgcolor='#ffffff' role='module-content'><div><div style='font-family: inherit; text-align: center'><span style='font-size: 43px'>Request Rejected By {awardee.Awardee?.ReportingPerson?.ReportingPerson?.FirstName}</span></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='a10dcb57-ad22-4f4d-b765-1d427dfddb4e' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:18px 30px 18px 30px; line-height:22px; text-align:inherit; background-color:#ffffff;' height='100%' valign='top' bgcolor='#ffffff' role='module-content'><div><div style='font-family: inherit; text-align: center'><span style='font-size: 18px'>Visit our site to </span><span style='color: #000000; font-size: 18px; font-family: arial, helvetica, sans-serif'>Get the Detailed information.</span><span style='font-size: 18px'>.</span></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffbe00; font-size: 18px'><strong>Thank you!</strong></span></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='7770fdab-634a-4f62-a277-1c66b2646d8d'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 20px 0px;' role='module-content' bgcolor='#ffffff'>
        </td>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' class='module' data-role='module-button' data-type='button' role='module' style='table-layout:fixed;' width='100%' data-muid='d050540f-4672-4f31-80d9-b395dc08abe1'>
      <tbody>
        <tr>
          <td align='center' bgcolor='#ffffff' class='outer-td' style='padding:0px 0px 0px 0px; background-color:#ffffff;'>
            <table border='0' cellpadding='0' cellspacing='0' class='wrapper-mobile' style='text-align:center;'>
              <tbody>
                <tr>
                <td align='center' bgcolor='#ffbe00' class='inner-td' style='border-radius:6px; font-size:16px; text-align:center; background-color:inherit;'>
                  <a href='' style='background-color:#ffbe00; border:1px solid #ffbe00; border-color:#ffbe00; border-radius:0px; border-width:1px; color:#000000; display:inline-block; font-size:14px; font-weight:normal; letter-spacing:0px; line-height:normal; padding:12px 40px 12px 40px; text-align:center; text-decoration:none; border-style:solid; font-family:inherit;' target='_blank'>Click to Visit</a>
                </td>
                </tr>
              </tbody>
            </table>
          </td>
        </tr>
      </tbody>
    </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='7770fdab-634a-4f62-a277-1c66b2646d8d.1'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 50px 0px;' role='module-content' bgcolor='#ffffff'>
        </td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='a265ebb9-ab9c-43e8-9009-54d6151b1600' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:50px 30px 50px 30px; line-height:22px; text-align:inherit; background-color:#6e6e6e;' height='100%' valign='top' bgcolor='#6e6e6e' role='module-content'><div><div style='font-family: inherit; text-align: center'><span style='color: #ffbe00; font-size: 18px'><strong>Here’s what we provide:</strong></span></div>
<div style='font-family: inherit; text-align: center'><br></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffffff; font-size: 18px'>1. Gain access to give a appreciation for the hardworking ones.</span></div>
<div style='font-family: inherit; text-align: center'><br></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffffff; font-size: 18px'>2. Make yourself to comment on your proud ones.</span></div>
<div style='font-family: inherit; text-align: center'><br></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffffff; font-size: 18px'>3. Get appreciated by all.</span></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffbe00; font-size: 18px'><strong>+ much more!</strong></span></div>
<div style='font-family: inherit; text-align: center'><br></div>
<div style='font-family: inherit; text-align: center'><span style='color: #ffffff; font-size: 18px'>Visit our site for more fun.</span></div>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' class='module' data-role='module-button' data-type='button' role='module' style='table-layout:fixed;' width='100%' data-muid='d050540f-4672-4f31-80d9-b395dc08abe1.1'>

    </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='c37cc5b7-79f4-4ac8-b825-9645974c984e'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 30px 0px;' role='module-content' bgcolor='6E6E6E'>
        </td>
      </tr>
    </tbody>
  </table></td>
        </tr>
      </tbody>
    </table></td>
      </tr>
    </tbody>
  </table>
    </table></td>
                                      </tr>
                                    </table>
                                    <!--[if mso]>
                                  </td>
                                </tr>
                              </table>
                            </center>
                            <![endif]-->
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
      </center>
    </body>
  </html>
";

            await File.WriteAllTextAsync(@"C:\Users\aakaash.mani\Desktop\Request_Rejected.html", text3);
        }




        public async Task ForgotAsync(Employee user)
        {
            string text4 =
                $@"<body>
      <center class='wrapper' data-link-color='#1188E6' data-body-style='font-size:14px; font-family:arial,helvetica,sans-serif; color:#000000; background-color:#ffb349;'>
        <div class='webkit'>
          <table cellpadding='0' cellspacing='0' border='0' width='100%' class='wrapper' bgcolor='#ffb349'>
            <tr>
              <td valign='top' bgcolor='#ffb349' width='100%'>
                <table width='100%' role='content-container' class='outer' align='center' cellpadding='0' cellspacing='0' border='0'>
                  <tr>
                    <td width='100%'>
                      <table width='100%' cellpadding='0' cellspacing='0' border='0'>
                        <tr>
                          <td>
                                    <table width='100%' cellpadding='0' cellspacing='0' border='0' style='width:100%; max-width:700px;' align='center'>
                                      <tr>
                                        <td role='modules-container' style='padding:0px 0px 0px 0px; color:#000000; text-align:left;' bgcolor='#F3F6FF' width='100%' align='left'><table class='module preheader preheader-hide' role='module' data-type='preheader' border='0' cellpadding='0' cellspacing='0' width='100%' style='display: none !important; mso-hide: all; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;'>
    <tr>
      <td role='module-content'>
        <p>Forgot Password</p>
      </td>
    </tr>
  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='17761d99-860c-4f1f-aeb0-be55796d7adf' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:15px 60px 10px 60px; line-height:12px; text-align:inherit; background-color:#ffb349;' height='100%' valign='top' bgcolor='#ffb349' role='module-content'><div><div style='font-family: inherit; text-align: center'></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' role='module' data-type='columns' style='padding:30px 30px 20px 30px;' bgcolor='#e2f1ff' data-distribution='1'>
    <tbody>
      <tr role='module-content'>
        <td height='100%' valign='top'><table width='280' style='width:280px; border-spacing:0; border-collapse:collapse; margin:0px 180px 0px 180px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor='' class='column column-0'>
      <tbody>
        <tr>
          <td style='padding:0px;margin:0px;border-spacing:0;'><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='8710905a-e942-4b46-a460-0799e4faf5c2'>
  </table></td>
        </tr>
      </tbody>
    </table></td>
      </tr>
    </tbody>
  </table><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='6e0d4f99-4ac1-47a7-8681-b39ea8ee64e3'>
    <tbody>
      <tr>
        <td style='font-size:6px; line-height:10px; padding:0px 0px 0px 0px;' valign='top' align='center'>
          <img class='max-width' border='0' style='display:block; color:#000000; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px; max-width:100% !important; width:100%; height:auto !important;' width='700' alt='' data-proportionally-constrained='true' data-responsive='true' src='http://cdn.mcauto-images-production.sendgrid.net/b841a9c8d79766a3/fbf4a65b-ebd9-4e08-a1b4-993cc67751d7/1748x1240.png'>
        </td>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' role='module' data-type='columns' style='padding:0px 5px 15px 5px;' bgcolor='' data-distribution='1'>
    <tbody>
      <tr role='module-content'>
        <td height='100%' valign='top'><table width='690' style='width:690px; border-spacing:0; border-collapse:collapse; margin:0px 0px 0px 0px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor='' class='column column-0'>
      <tbody>
        <tr>
          <td style='padding:0px;margin:0px;border-spacing:0;'><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='a5ee1b9d-aacf-476c-ad72-fa1f2d816f12' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:30px 0px 0px 0px; line-height:50px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div><h1 style='text-align: center; font-family: inherit'><span style='font-family: &quot;lucida sans unicode&quot;, &quot;lucida grande&quot;, sans-serif; font-size: 60px; color: #1c2c7b'><strong>Have you forgot your Password ?</strong></span></h1><div></div></div></td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='a5ee1b9d-aacf-476c-ad72-fa1f2d816f12.2' data-mc-module-version='2019-10-22'>

  </table></td>
        </tr>
      </tbody>
    </table></td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='9fad6950-fb2c-4904-8bf1-426398c84a27'>
    <tbody>
      <tr>
        <td style='padding:0px 0px 15px 0px;' role='module-content' bgcolor=''>
        </td>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' role='module' data-type='columns' style='padding:0px 20px 20px 20px;' bgcolor='#F3F6FF' data-distribution='1'>
    <tbody>
      <tr role='module-content'>
        <td height='100%' valign='top'><table width='460' style='width:460px; border-spacing:0; border-collapse:collapse; margin:0px 100px 0px 100px;' cellpadding='0' cellspacing='0' align='left' border='0' bgcolor='' class='column column-0'>
      <tbody>
        <tr>
          <td style='padding:0px;margin:0px;border-spacing:0;'><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='a5ee1b9d-aacf-476c-ad72-fa1f2d816f12.1' data-mc-module-version='2019-10-22'>
    <tbody>
      <tr>
        <td style='padding:30px 30px 30px 30px; line-height:30px; text-align:inherit; background-color:#75c6fb;' height='100%' valign='top' bgcolor='#75c6fb' role='module-content'><div><div style='font-family: inherit; text-align: center'><span style='font-family: &quot;lucida sans unicode&quot;, &quot;lucida grande&quot;, sans-serif; font-size: 22px; color: #1c2c7b'></span></div>
<div style='font-family: inherit; text-align: center'><span style='font-family: &quot;lucida sans unicode&quot;, &quot;lucida grande&quot;, sans-serif; font-size: 22px; color: #1c2c7b'>Your Password is mentioned below</span></div>
<div style='font-family: inherit; text-align: center'></div><div></div></div></td>
      </tr>
    </tbody>
  </table><table border='0' cellpadding='0' cellspacing='0' class='module' data-role='module-button' data-type='button' role='module' style='table-layout:fixed;' width='100%' data-muid='71d66b9a-f3bc-4b7a-97f3-479258b379cf'>
      <tbody>
        <tr>
          <td align='center' bgcolor='#F3F6FF' class='outer-td' style='padding:25px 0px 25px 0px; background-color:#F3F6FF;'>
            <table border='0' cellpadding='0' cellspacing='0' class='wrapper-mobile' style='text-align:center;'>
              <tbody>
                <tr>
                <td align='center' bgcolor='#D53B3E' class='inner-td' style='border-radius:6px; font-size:16px; text-align:center; background-color:inherit;'>
                 <div style='background-color:#D53B3E; border:0px solid #333333; border-color:#333333; border-radius:0px; border-width:0px; display:inline-block; font-size:16px; font-weight:700; letter-spacing:2px; line-height:normal; padding:15px 50px 15px 50px; text-align:center; text-decoration:none; border-style:solid; font-family:lucida sans unicode,lucida grande,sans-serif; color:#ffffff;'>Password</span></div>
                </td>
                </tr>
              </tbody>
            </table>
          </td>
        </tr>
      </tbody>
    </table></td>
        </tr>
      </tbody>
    </table></td>
      </tr>
    </tbody>
  </table><table class='module' role='module' data-type='social' align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='00228565-20cc-44a6-8867-4f1a277e9588'>

          </table>
        </td>
      </tr>
    </tbody>
  </table><div data-role='module-unsubscribe' class='module' role='module' data-type='unsubscribe' style='background-color:#FFB349; color:#404D8C; font-size:12px; line-height:20px; padding:16px 16px 5px 16px; text-align:Center;' data-muid='4e838cf3-9892-4a6d-94d6-170e474d21e5'><p style='font-family:lucida sans unicode,lucida grande,sans-serif; font-size:12px; line-height:20px;'><a class='Unsubscribe--unsubscribeLink' target='_blank' style='color:#F3F6FF;'>Tenacious.</a></p></div><table border='0' cellpadding='0' cellspacing='0' class='module' data-role='module-button' data-type='button' role='module' style='table-layout:fixed;' width='100%' data-muid='0acf1c9a-9616-4586-aa54-a24c223ed970'>
    </table></td>
                                      </tr>
                                    </table>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </div>
      </center>
    </body>
  </html>";

            await File.WriteAllTextAsync(@"C:\Users\aakaash.mani\Desktop\Forgot_Password.html", text4);
        }
    }
}