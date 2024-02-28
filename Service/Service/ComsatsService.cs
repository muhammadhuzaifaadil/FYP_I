using Core.Data.DataContext;
using Core.Data.Entities;
using HtmlAgilityPack;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitOfWork;

namespace Service.Service
{
    public class ComsatsService : IComsatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public ComsatsService(IUnitOfWork unitOfWork,ApplicationDbContext context) 
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public List<UniversityCalendar> GetUniversityCalendars()
        {
            var UniversityCalendars = _context.UniversityCalendar.Where(x => x.UId == 2).ToList();
            if (UniversityCalendars.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://lahore.comsats.edu.pk/admissions/admissions-schedule.aspx");
                var masternodeElement = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-striped table-bordered text-center align-middle']");
                var EventTitle = "";
                var EventDeadline = "";
                var Count = 0;
                if (masternodeElement != null)
                {
                    var tbodyElement = masternodeElement.SelectSingleNode(".//tbody");
                    if (tbodyElement != null)
                    {
                        var trElements = tbodyElement.SelectNodes(".//tr");
                        if (trElements != null)
                        {
                            foreach (var trElement in trElements)
                            {
                                if (Count <= 3)
                                {
                                    var tdElements = trElement.SelectNodes(".//td");
                                    if (tdElements != null)
                                    {
                                        var tdInnerText = tdElements[0]?.InnerText;

                                        // Check if there is an <a> tag inside the td element
                                        var aTagInnerText = tdElements[0]?.SelectSingleNode(".//a")?.InnerText;

                                        // If there is an <a> tag, exclude its inner text from the td element's inner text
                                        if (aTagInnerText != null)
                                        {
                                            // Remove the inner text of the <a> tag from the td element's inner text
                                            tdInnerText = tdInnerText?.Replace(aTagInnerText, "");
                                        }
                                        EventTitle = WebUtility.HtmlDecode(Regex.Replace(tdInnerText, @"\s+", " "));
                                        EventDeadline = WebUtility.HtmlDecode(Regex.Replace(tdElements[1].InnerText.Trim(), @"\s+", " "));
                                        var UniversityCalendar = new UniversityCalendar
                                        {
                                            EventTitle = EventTitle,
                                            EventDeadline = EventDeadline,
                                            EventDetails = "No",
                                            EventNotification = "no",
                                            UId = 2
                                        };
                                        Count++;
                                        UniversityCalendars.Add(UniversityCalendar);
                                    }
                                }
                            }
                        }
                    }
                    _unitOfWork.UniversityCalendarRepository.AddRange(UniversityCalendars);
                    _unitOfWork.UniversityCalendarRepository.SaveChanges();

                }
            }
            return UniversityCalendars;
        }

        public List<UniversityDepartments> GetUniversityDepartments()
        {
            var UniversityDepartments = _context.UniversityDepartment.Where(x => x.UId == 2).ToList();
            if (UniversityDepartments.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://www.comsats.edu.pk/AcademicPrograms.aspx");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-hover table-sm table-bordered']");
                if (nodeElement != null)
                {
                    var tbodyElement = nodeElement.SelectSingleNode(".//tbody");
                    if (tbodyElement != null)
                    {
                        var trNodes = tbodyElement.SelectNodes(".//tr");
                        if (trNodes.Count > 1)
                        {
                            for (int i = 1; i < trNodes.Count; i++)
                            {
                                var trNode = trNodes[i];
                                var tdNodes = trNode.SelectNodes(".//td");
                                if (tdNodes.Count > 1)
                                {
                                    var secondTdNodeInnerText = WebUtility.HtmlDecode(tdNodes[1].InnerText?.Trim());
                                    var IslamabadtdNodeInnerText = tdNodes[2].InnerText?.Trim();
                                    var AbbottabadtdNodeInnerText = tdNodes[3].InnerText?.Trim();
                                    var WahtdNodeInnerText = tdNodes[4].InnerText?.Trim();
                                    var LahoretdNodeInnerText = tdNodes[5].InnerText?.Trim();
                                    var AttocktdNodeInnerText = tdNodes[6].InnerText?.Trim();
                                    var SahiwaltdNodeInnerText = tdNodes[7].InnerText?.Trim();
                                    var VeharitdNodeInnerText = tdNodes[8].InnerText?.Trim();
                                    //var Campus="";
                                    var campusNamesList = new List<string>();
                                    if (IslamabadtdNodeInnerText == "&#10004;")
                                    {
                                        //Campus = "Islamabad";
                                        campusNamesList.Add("Islamabad");
                                    }
                                    if (AbbottabadtdNodeInnerText == "&#10004;")
                                    {
                                        campusNamesList.Add("Abbottabad");
                                    }
                                    if (WahtdNodeInnerText == "&#10004;")
                                    {
                                        campusNamesList.Add("Wah");
                                    }
                                    if (LahoretdNodeInnerText == "&#10004;")
                                    {
                                        campusNamesList.Add("Lahore");
                                    }
                                    if (AttocktdNodeInnerText == "&#10004;")
                                    {
                                        campusNamesList.Add("Attock");
                                    }
                                    if (SahiwaltdNodeInnerText == "&#10004;")
                                    {
                                        campusNamesList.Add("Sahiwal");
                                    }
                                    if (VeharitdNodeInnerText == "&#10004;")
                                    {
                                        campusNamesList.Add("Vehari");
                                    }
                                    var Department = new UniversityDepartments
                                    {
                                        DepartmentName = secondTdNodeInnerText,
                                        UId = 2
                                    };
                                    Department.Campus = string.Join(",", campusNamesList);
                                    UniversityDepartments.Add(Department);

                                }
                            }
                        }
                    }
                    _unitOfWork.UniversityDepartmentRepository.AddRange(UniversityDepartments);
                    _unitOfWork.UniversityDepartmentRepository.SaveChanges();

                }
            }
            return UniversityDepartments;
        }

        public List<UniversityDocuments> GetUniversityDocuments()
        {
           var UniversityDocuments = _context.UniversityDocument.Where(x=>x.UId == 2).ToList();
            if(UniversityDocuments.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://ww2.comsats.edu.pk/ms_wah/AdmissionEligibilityBSAF.aspx");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='col-lg-9']");
                if(nodeElement!= null)
                {
                    var ulElement = nodeElement.SelectSingleNode(".//ul");
                    if(ulElement!= null)
                    {
                        var liElements = ulElement.SelectNodes(".//li");
                        if (liElements != null)
                        {
                            foreach(var liElement in liElements)
                            {
                                var liElementText = liElement.InnerText.Trim();
                                var UniversityDocument = new UniversityDocuments
                                {
                                    DocumentRequirement = liElementText,
                                    UId = 2,
                                };
                                UniversityDocuments.Add(UniversityDocument);
                            }
                        }
                    }
                }
                _unitOfWork.UniversityDocumentRepository.AddRange(UniversityDocuments);
                _unitOfWork.UniversityDocumentRepository.SaveChanges();
            }
            return UniversityDocuments;
        }

        public List<UniversityFee> GetUniversityFee()
        {
            var UniversityFees = _context.UniversityFee.Where(x => x.UId == 2).ToList();
            if(UniversityFees.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://lahore.comsats.edu.pk/admissions/fee.aspx");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-striped table-bordered text-center align-middle']");

                if (nodeElement != null)
                {
                    var tbodyElement = nodeElement.SelectSingleNode("//tbody");
                    if (tbodyElement != null)
                    {
                        var trElements = tbodyElement.SelectNodes("//tr");
                        if (trElements != null)
                        {
                            var admissionRowElement = trElements[1];
                            var semesterFeeElement = trElements[6];
                            var admissionFees = "";
                            var semesterFees = "";
                            var semesterFeeValuesOnly = "";
                            int semesterFeesValue = 0;
                            if (admissionRowElement != null)
                            {
                                var tdadElements = admissionRowElement.SelectNodes("//td");
                                if (tdadElements != null)
                                {
                                    admissionFees = tdadElements[8].InnerText.Trim();
                                }
                            }
                            if (semesterFeeElement != null)
                            {
                                var tdsemElements = semesterFeeElement.SelectNodes("//td");
                                if (tdsemElements != null)
                                {
                                    semesterFees = tdsemElements[33].InnerText.Trim();
                                    semesterFeeValuesOnly = semesterFees.Replace(",", "");//replaces the comma
                                    semesterFeesValue = int.Parse(semesterFeeValuesOnly) / 16; // converts the string to int and divide by cr hour
                                }
                            }
                            var FeeDetail = new UniversityFee
                            {
                                AdmissionFee = admissionFees,
                                PerCreditHourFee = semesterFeesValue.ToString(),
                                UId = 2
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
