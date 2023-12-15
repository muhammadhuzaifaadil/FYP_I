using Core.Data.DataContext;
using Core.Data.Entities;
using HtmlAgilityPack;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace Service.Service
{
    public class LUMSService : ILUMSService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public LUMSService(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public List<UniversityCalendar> GetUniversityCalendars()
        {
            var UniversityCalendars = _context.UniversityCalendar.Where(x => x.UId == 4).ToList();
            if(UniversityCalendars.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://admission.lums.edu.pk/critical-dates-all-programmes");
                var masternodeElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='quicktabs-tabpage-admissions-calendar-0']");
                //var nodeElement = htmlDoc.DocumentNode.SelectNodes("//div[@class='date-month-container2']");
                var EventTitle = "";
                var EventDeadline = "";

                if (masternodeElement != null)
                {

                    var nodeElement = masternodeElement.SelectNodes(".//div[@class='date-month-container2']");//no dot means regardless of masternode checks from root node

                    if (nodeElement != null)
                    {
                        foreach (var node in nodeElement)
                        {
                            var divElements = node.SelectNodes(".//div");
                            if (divElements != null)
                            {
                                EventTitle = divElements[0].InnerText.Trim();
                                EventDeadline = divElements[1].InnerText.Trim();

                                var UniversityCalendar = new UniversityCalendar
                                {
                                    EventTitle = EventTitle,
                                    EventDeadline = EventDeadline,
                                    EventDetails = "No FurtherInfo",
                                    EventNotification = "Notify",
                                    UId = 4
                                };
                                UniversityCalendars.Add(UniversityCalendar);
                            }

                        }
                    }
                }
                _unitOfWork.UniversityCalendarRepository.AddRange(UniversityCalendars);
                _unitOfWork.UniversityCalendarRepository.SaveChanges();
            }
            return UniversityCalendars;
        }

        public List<UniversityDepartments> GetUniversityDepartments()
        {
            var UniversityDepartments = _context.UniversityDepartment.Where(x => x.UId == 4).ToList();
            if( UniversityDepartments.Count==0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://lums.edu.pk/undergraduate");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='item-list']");
                if (nodeElement != null)
                {
                    var ulElement = nodeElement.SelectSingleNode(".//ul");
                    if (ulElement != null)
                    {
                        var liElements = ulElement.SelectNodes(".//li");
                        if (liElements != null)
                        {
                            foreach (var liElement in liElements)
                            {
                                var divElement = liElement.SelectSingleNode(".//div");
                                if (divElement != null)
                                {
                                    var spanElement = divElement.SelectSingleNode(".//span");
                                    if (spanElement != null)
                                    {
                                        var div2Element = spanElement.SelectSingleNode(".//div");
                                        if (div2Element != null)
                                        {
                                            var aElement = div2Element.SelectSingleNode(".//a");
                                            if (aElement != null)
                                            {
                                                var divsElements = aElement.SelectNodes(".//div");
                                                if (divsElements != null)
                                                {
                                                    var DepName = WebUtility.HtmlDecode(divsElements[1].InnerText?.Trim());
                                                    var UniversityDepartment = new UniversityDepartments
                                                    {
                                                        DepartmentName = DepName,
                                                        Campus = "Lahore",
                                                        UId=4
                                                    };
                                                    UniversityDepartments.Add(UniversityDepartment);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                _unitOfWork.UniversityDepartmentRepository.AddRange(UniversityDepartments);
                _unitOfWork.UniversityDepartmentRepository.SaveChanges();
            }
            return UniversityDepartments;
        }

        public List<UniversityDocuments> GetUniversityDocuments()
        {
            throw new NotImplementedException();
        }

        public List<UniversityFee> GetUniversityFee()
        {
            var UniversityFees = _context.UniversityFee.Where(x => x.UId == 4).ToList();
            if(UniversityFees.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://lums.edu.pk/programmes/bs-computer-science#b");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='Table']");
                var creditHr = "";
                var admissionFee = "";
                var semesterFee = "";
                var semesterFeeValuesOnly = "";
                int semesterFeeINT = 0;
                int creditHrINT = 0;
                if (nodeElement != null)
                {
                    var tbodyElement = nodeElement.SelectSingleNode("//tbody");
                    if (tbodyElement != null)
                    {
                        var trElements = tbodyElement.SelectNodes("//tr");
                        if (trElements != null)
                        {
                            var credittrElement = trElements[1];
                            var admissionFeetrElement = trElements[3];
                            var semesterFeetrElement = trElements[4];
                            if (credittrElement != null)
                            {
                                var credittdElements = credittrElement.SelectNodes("//td");
                                if (credittdElements != null)
                                {
                                    creditHr = credittdElements[24].InnerText.Trim();
                                    creditHrINT = int.Parse(creditHr);
                                    //if(credittdElement != null)
                                    //{
                                    //    var credittdstrongElement = credittdElement.SelectSingleNode("//strong");
                                    //    if( credittdstrongElement != null)
                                    //    {
                                    //        creditHr = credittdstrongElement.InnerText.Trim();
                                    //    }
                                    //}
                                }
                            }
                            if (admissionFeetrElement != null)
                            {
                                var admissiontdElements = admissionFeetrElement.SelectNodes("//td");
                                if (admissiontdElements != null)
                                {
                                    admissionFee = admissiontdElements[34].InnerText.Trim();
                                    //if (admissiontdElement != null)
                                    //{
                                    //    var admissionpElement = admissiontdElement.SelectSingleNode("//p");
                                    //    if(admissionpElement != null)
                                    //    {
                                    //        var admissionspanElement = admissionpElement.SelectSingleNode("//span");
                                    //        if(admissionspanElement != null)
                                    //        {
                                    //            admissionFee = admissionspanElement.InnerText.Trim();
                                    //        }
                                    //    }
                                    //}
                                }
                            }
                            if (semesterFeetrElement != null)
                            {
                                var semestertdElements = semesterFeetrElement.SelectNodes("//td");
                                if (semestertdElements != null)
                                {
                                    var semestertdElement = semestertdElements[39];
                                    if (semestertdElement != null)
                                    {
                                        semesterFee = semestertdElement.InnerText.Trim();
                                        semesterFeeValuesOnly = semesterFee.Replace(",", "");
                                        semesterFeeINT = int.Parse(semesterFeeValuesOnly) / creditHrINT;
                                    }
                                }
                            }
                            var FeeDetail = new UniversityFee
                            {
                                AdmissionFee = admissionFee,
                                PerCreditHourFee = semesterFeeINT.ToString(),
                                UId= 4
                            };
                            UniversityFees.Add(FeeDetail);
                        }
                    }

                }
                _unitOfWork.UniversityFeeRepository.AddRange(UniversityFees);
                _unitOfWork.UniversityFeeRepository.SaveChanges();
            }
            return UniversityFees;
        }
    }
}
