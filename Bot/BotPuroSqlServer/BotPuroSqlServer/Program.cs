using BotPuroSqlServer;
using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotVinculacionUnitec
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("1099955313:AAE4MUcmOzK09op7z8K-K5VNANtumC2n9WQ");

        public static Cache MemoryCacheRunning = new  Cache();

        static void Main(string[] args)
        {
           

            //Método que se ejecuta cuando se recibe un mensaje
            Bot.OnMessage += Bot_OnMessage; ;

            //Método que se ejecuta cuando se recibe un callbackQuery
            Bot.OnCallbackQuery += Bot_OnCallbackQuery; ;

            //Método que se ejecuta cuando se recibe un error
            Bot.OnReceiveError += Bot_OnReceiveError; ;
            Bot.StartReceiving();
            Console.WriteLine("Bot Encendido y Recibiendo @VinculacionUnitecsps_bot");

            Console.ReadLine();
            Bot.StopReceiving();
        }


        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;

            DatabaseQueries informationGathering = new DatabaseQueries();

            Console.WriteLine($"Mensaje de @{message.Chat.Username}:" + message.Text);


            if (message == null || message.Type != Telegram.Bot.Types.Enums.MessageType.Text) return; ;

            bool exists = informationGathering.ExisteDb(message.Chat.Username);
            bool estate = informationGathering.estadoDb(message.Chat.Username);
            //Prueba oara dia actual
            // sqlite.FechaA();
            if (message.Chat.Username == null) {
                Bot.SendTextMessageAsync(message.Chat.Id, "Su cuenta de Telegram no esta configurada correctamente porfavor\n Ve a Ajustes>Elegir Nombre de Usuario");
            }
            else {
                if (exists && estate)
                {
                    string numCuenta = informationGathering.GetCuentaNUMDb(message.Chat.Username);

                    string nombre = informationGathering.GetCuentaDb(message.Chat.Username);
                    //Declaracion Botones
                    var BotonesHYD = new InlineKeyboardMarkup(new[]
                  {
                                        new []
                                      {
                                 InlineKeyboardButton.WithCallbackData(
                                  text:"Horas Totales",
                                  callbackData: "Horas "+ numCuenta),//Aqui mando el numero de Cuenta para devolver al usuario indicado
                                    InlineKeyboardButton.WithCallbackData(
                                       text:"Detalle de Horas",
                                      callbackData: "Detalles "+numCuenta)
                                          }});
                    //INformacion botones

                    // MOstrar Botones  
                    Bot.SendTextMessageAsync(
                      message.Chat.Id,
                      "Estimado estudiante " + nombre + ": Bienvenido al Asistente de Vinculación UNITEC-SPS \n Elija una Opcion",
                      replyMarkup: BotonesHYD);
                }
                else if (exists && estate == false)
                {



                    if (informationGathering.VerificarDb(message.Chat.Username, message.Text.Split(" ").Last().ToString()))
                    {

                        bool verifcadoCorrectamen = informationGathering.VerificarUpdateDb(message.Chat.Username, message.Text.Split(" ").Last().ToString());
                        if (verifcadoCorrectamen)
                        {
                            Bot.SendTextMessageAsync(message.Chat.Id, "Tu cuenta se Verifico Exitosamente");
                            //Declaracion Botones
                            string numCuenta = informationGathering.GetCuentaNUMDb(message.Chat.Username);
                            string nombre = informationGathering.GetCuentaDb(message.Chat.Username);
                            var BotonesHYD = new InlineKeyboardMarkup(new[]
                              {
                                        new []
                                      {
                                 InlineKeyboardButton.WithCallbackData(
                                  text:"Horas Totales",
                                  callbackData: "Horas "+ numCuenta),//Aqui mando el numero de Cuenta para devolver al usuario indicado
                                    InlineKeyboardButton.WithCallbackData(
                                       text:"Detalle de Horas",
                                      callbackData: "Detalles "+numCuenta)
                                          }});
                            //INformacion botones

                            // MOstrar Botones  
                            Bot.SendTextMessageAsync(
                              message.Chat.Id,
                              "Estimado estudiante " + nombre + ": Bienvenido al Asistente de Vinculación UNITEC-SPS \n Elija una Opcion",
                              replyMarkup: BotonesHYD);
                        }


                    }
                    else if (message.Text.Split(" ").Last().ToString().ToLower() == "reenviar")
                    {

                        // Obtengo el correo de cuenta actual que quiere que le reenvie codigo
                        string cuenta = informationGathering.GetCuentaNUMDb(message.Chat.Username);
                        string Mail = informationGathering.GetMailDb(cuenta);



                        // oculto ciertas partes del corroe para que no sea visible en su totalidad
                        string changedMail = informationGathering.ConverMail(Mail);
                        //envio respeusta de donde envie su codigo de confirmacion
                        if (informationGathering.newTokenDb(message.Chat.Username, Mail))
                        {
                            Console.WriteLine("Se reenvio un nuevo codigo a de confimacion al correo:" + changedMail);
                            Bot.SendTextMessageAsync(message.Chat.Id, "Se reenvio un nuevo codigo a de confimacion al correo:" + changedMail);
                        }
                        else
                        {
                            Bot.SendTextMessageAsync(message.Chat.Id, "Para Solicitar un nuevo codigo debes tener mas de 5 minutos de haber recibido tu ultimo codigo ");
                        }


                    }

                    else
                    {

                        Bot.SendTextMessageAsync(message.Chat.Id, "Token Ingresado Incorrecto\nEscribe reenviar para solicitar nuevo token");

                    }

                }
                else
                {


                    switch (message.Text.Split(" ").First().ToLower())
                    {
                        case "/start":


                            Bot.SendTextMessageAsync(message.Chat.Id,
                                     "Estimado estudiante: Bienvenido al Asistente de Vinculación UNITEC-SPS\n\nIngrese su número de cuenta para sus consultas");

                            break;



                        default:

                            switch (!informationGathering.CuentaExisteDb(message.Text.Split(" ").First().ToString()))
                            {

                                case false:
                                    // revisa que el numero de cuenta venga solo sin ningun otra palabra
                                    if (informationGathering.CuentaExisteDb(message.Text.Split(" ").Last().ToString()))
                                    {
                                        // verificar si la cuenta esta verificada
                                        switch (informationGathering.AccountVerified(message.Text))
                                        {
                                            //Caso cuenta verificada pero con otro user de telegram
                                            case true:

                                                Bot.SendTextMessageAsync(message.Chat.Id, "El numero de Cuenta que ingresaste ya esta ligado a otra cuenta de telegram \nPara consultas enviar correo a:\nvinculacionsps@unitec.edu ó andrea.orellana@unitec.edu.hn");

                                                break;//case cuenta verificada con otro user de telegram

                                            //Caso Cuenta no verificada
                                            case false:

                                                // Obtengo el correo de cuenta ingresada
                                                string Mail = informationGathering.GetMailDb(message.Text);
                                                // oculto ciertas partes del corroe para que no sea visible en su totalidad
                                                string changedMail = informationGathering.ConverMail(Mail);
                                                //envio respeusta de donde envie su codigo de confirmacion




                                                //Proceso de enviar correo y generar token
                                                if (Mail == null || Mail == "" || Mail == " " || Mail == "NULL")
                                                {
                                                    Bot.SendTextMessageAsync(message.Chat.Id, "Tu Numero de cuenta no tiene un correo vinculado \nPara consultas enviar correo a:\nvinculacionsps@unitec.edu ó andrea.orellana@unitec.edu.hn");

                                                }
                                                else {
                                                    Bot.SendTextMessageAsync(message.Chat.Id, "Numero de Cuenta no verificado \nPara verificar Tu Numero de Cuenta ingresas token enviado a tu correo: " + changedMail);

                                                    informationGathering.insertarDb(message.Chat.Username, message.Text, Mail);
                                                    Console.WriteLine("Se ha reenviado un codigo de Verificacion al correo:" + Mail);

                                                }

                                                break;


                                        }
                                    }
                                    break;

                                default:


                                    Bot.SendTextMessageAsync(message.Chat.Id, "Numero de Cuenta Incorrecto Vuelve a ingresarlo Para consultas enviar correo a:\nvinculacionsps@unitec.edu ó andrea.orellana@unitec.edu.hn");


                                    break;
                            }


                            break;
                    }
                }

            }
        }


        private static void Bot_OnReceiveError(object sender, ReceiveErrorEventArgs e)
        {

            Console.WriteLine(e.ApiRequestException.Message);
        }

        private static void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            DatabaseQueries sqlserver = new DatabaseQueries();
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;
            switch (callbackQuery.Data.Split(" ").First().ToLower())
            {

                case "horas":
                    string hours = " ";
                    if (!MemoryCacheRunning.existInCache(callbackQuery.Message.Chat.Username))
                    {
                        DatabaseQueries informationGathering = new DatabaseQueries();
                        string StudentName = informationGathering.GetCuentaDb(callbackQuery.Message.Chat.Username);
                        string telegramUser = callbackQuery.Message.Chat.Username;
                        string accouNumber = callbackQuery.Data.Split(" ").Last();
                         hours = informationGathering.getTotalHoras(callbackQuery.Data.Split(" ").Last());
                        string detailHours = informationGathering.HorasDetalle2(callbackQuery.Data.Split(" ").Last());
                        MemoryCacheRunning.addToCache(telegramUser, callbackQuery.Data.Split(" ").Last(), StudentName, detailHours, hours);
                    }
                    else
                    {
                         hours= MemoryCacheRunning.getHours(callbackQuery.Data.Split(" ").Last());

                    }
                    ///vuelve a mostrar el boton

                    var BotonesHYD = new InlineKeyboardMarkup(new[]
                          {
                                        new []
                                      {
                                 InlineKeyboardButton.WithCallbackData(
                                  text:"Horas Totales",
                                  callbackData: "Horas "+ callbackQuery.Data.Split(" ").Last()),//Aqui mando el numero de Cuenta para devolver al usuario indicado
                                    InlineKeyboardButton.WithCallbackData(
                                       text:"Detalle de Horas",
                                      callbackData: "Detalles "+callbackQuery.Data.Split(" ").Last())
                                          }});
                    //vuelve a mostrar boton

                    Bot.SendTextMessageAsync(
                     callbackQuery.Message.Chat.Id,
                     "Tienes un total de " + hours + " horas a la fecha.\nPara consultas enviar correo a:\nvinculacionsps@unitec.edu ó andrea.orellana@unitec.edu.hn\n\nOpciones ",
                     replyMarkup: BotonesHYD);

                    break;

                case "detalles":

                    string DetalleHoras = " ";

                    if (!MemoryCacheRunning.existInCache(callbackQuery.Message.Chat.Username))
                    {
                        DatabaseQueries informationGathering = new DatabaseQueries();
                        string StudentName = informationGathering.GetCuentaDb(callbackQuery.Message.Chat.Username);
                        string telegramUser = callbackQuery.Message.Chat.Username;
                        string accountNumber = callbackQuery.Data.Split(" ").Last();
                        string hour = informationGathering.getTotalHoras(callbackQuery.Data.Split(" ").Last());
                        string detailHours = informationGathering.HorasDetalle2(callbackQuery.Data.Split(" ").Last());
                        MemoryCacheRunning.addToCache(telegramUser, accountNumber, StudentName,detailHours,hour);
                    }
                    else
                    {
                        DetalleHoras = MemoryCacheRunning.getHoursDetails(callbackQuery.Data.Split(" ").Last());
                    
                    }

                    var BotonesHY = new InlineKeyboardMarkup(new[]
                          {
                                         new []
                                       {
                                  InlineKeyboardButton.WithCallbackData(
                                   text:"Horas Totales",
                                   callbackData: "Horas "+ callbackQuery.Data.Split(" ").Last()),//Aqui mando el numero de Cuenta para devolver al usuario indicado
                                     InlineKeyboardButton.WithCallbackData(
                                        text:"Detalle de Horas",
                                       callbackData: "Detalles "+callbackQuery.Data.Split(" ").Last())
                                           }});
                    

                    Bot.SendTextMessageAsync(
                   callbackQuery.Message.Chat.Id,
                    "Tu Informacion es la siguiente:\n" + DetalleHoras + "\nPara consultas enviar correo a:\nvinculacionsps@unitec.edu ó andrea.orellana@unitec.edu.hn\n\nOpciones",
                    replyMarkup: BotonesHY);

                    break;


                default:
                    break;
            }

        }

       
    }




}
