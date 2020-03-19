using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace LeerMensajesBotTelegram
{
    class Program
    {
        //Conectamos con el bot de Telegram usando el token recibido al crearlo
        private static readonly TelegramBotClient miBotTelegram = new TelegramBotClient("1099955313:AAE4MUcmOzK09op7z8K-K5VNANtumC2n9WQ");

        public static string nombreBot;

        public static void Main(string[] args)
        {
            //Escuchamos los mensajes enviados en los grupos donde esté el bot
            var me = miBotTelegram.GetMeAsync().Result;
            nombreBot = me.Username;
            Console.Title = $"Conectado a bot de Telegram @{nombreBot}...";

            //Asignamos los eventos de lectura de mensajes y captura de errores
            miBotTelegram.OnMessage += EventoBotTelegramLeerMensajes;
            miBotTelegram.OnReceiveError += EventoBotTelegramErrorRecibido;

            //Iniciamos la lectura de mensajes
            miBotTelegram.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Escuchando mensajes de @{nombreBot}...");

            //Si se pulsa INTRO en la consola se detiene la escucha de mensajes
            Console.ReadLine();
            miBotTelegram.StopReceiving();
        }

        //Evento que lee los mensajes de los grupos donde esté el bot de Telegram        
        private static async void EventoBotTelegramLeerMensajes(object sender,
            MessageEventArgs eventoArgumentosMensajeRecibido)
        {
            var mensaje = eventoArgumentosMensajeRecibido.Message;

            if (mensaje == null || mensaje.Type != MessageType.Text)
                return;

            Console.WriteLine($"Mensaje de @{mensaje.Chat.Username}:" + mensaje.Text);
            var nCuenta = mensaje.Text;
            if(nCuenta!="")
            {
                Console.WriteLine(nCuenta);
                if (enviarCorreo(nCuenta))
                {
                    Console.WriteLine("Correo Enviado con Exito");
                }
                else
                {
                    Console.WriteLine("ERROR");
                }
            } 

        }

        private static void EventoBotTelegramErrorRecibido(object sender, ReceiveErrorEventArgs eventoArgumentosErrorRecibidos)
        {
            Console.WriteLine("Error recibido: {0} — {1}",
                eventoArgumentosErrorRecibidos.ApiRequestException.ErrorCode,
                eventoArgumentosErrorRecibidos.ApiRequestException.Message);
        }
        private static bool enviarCorreo(string correo)
        {
            var codigo = "2111";
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("nunez0467@gmail.com"));
            email.From = new MailAddress("nunez0467@gmail.com");
            email.Subject = "CONFIRMACION ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            email.Body = "El codigo de Confirmacion es : " + codigo;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.google.com";
            smtp.Port = 2525;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("nunez0467@gmail.com", "Novemberrain25");

            string output = null;
            try
            {
                smtp.Send(email);
                email.Dispose();
                Console.WriteLine("Correo Enviado con Exito");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}