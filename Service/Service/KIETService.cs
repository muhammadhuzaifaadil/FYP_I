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
using System.Xml.Linq;
using UnitOfWork;

namespace Service.Service
{
    public class KIETService:IKIETService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public KIETService(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        //public List<UniversityCalendar> GetUniversityCalendars()
        //{
        //    var UniversityCalendars = _context.UniversityCalendar.Where(x => x.UId == 3).ToList();
        //    if(UniversityCalendars.Count == 0 )
        //    {
        //        var web = new HtmlWeb();
        //        var htmlDoc = web.Load("https://admissions.kiet.edu.pk/admission-schedule/");
        //        var masternodeElement = htmlDoc.DocumentNode.SelectNodes("//div[@class='wpb_wrapper']");
        //        var EventTitle = "";
        //        var EventDeadline = "";
        //        var EventDetails = "";
        //        var Count = 0;

        //        if (masternodeElement != null)
        //        {
        //            var nodeElement = masternodeElement[9];
        //            if (nodeElement != null)
        //            {
        //                var divElement = nodeElement.SelectSingleNode(".//div");
        //                if (divElement != null)
        //                {
        //                    var divElement2 = divElement.SelectSingleNode(".//div");
        //                    if (divElement2 != null)
        //                    {
        //                        var divElement3 = divElement2.SelectSingleNode(".//div");
        //                        if (divElement3 != null)
        //                        {
        //                            var pElements = divElement3.SelectNodes(".//p");
        //                            if (pElements != null)
        //                            {
        //                                foreach (var pElement in pElements)
        //                                {
        //                                    if (Count <= 2)
        //                                    {
        //                                        EventDetails = WebUtility.HtmlDecode(pElement.InnerText.Trim());


        //                                        var AcademicCalendar = new UniversityCalendar
        //                                        {
        //                                            EventTitle = EventTitle,
        //                                            EventDeadline = EventDeadline,
        //                                            EventDetails = EventDetails,
        //                                            EventNotification = "no",
        //                                            UId = 3
        //                                        };
        //                                        Count++;
        //                                        UniversityCalendars.Add(AcademicCalendar);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                _unitOfWork.UniversityCalendarRepository.AddRange(UniversityCalendars);
        //                _unitOfWork.UniversityCalendarRepository.SaveChanges();
        //            }
        //        }
        //    }
        //    return UniversityCalendars;
        //}
        public List<UniversityCalendar> GetUniversityCalendars()
        {
            var UniversityCalendars = _context.UniversityCalendar.Where(x => x.UId == 3).ToList();
            if (UniversityCalendars.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://admissions.kiet.edu.pk/admission-schedule/");
                var masternodeElement = htmlDoc.DocumentNode.SelectNodes("//div[@class='wpb_wrapper']");
                var Count = 0;

                if (masternodeElement != null)
                {
                    var nodeElement = masternodeElement[9];
                    if (nodeElement != null)
                    {
                        var divElement = nodeElement.SelectSingleNode(".//div");
                        if (divElement != null)
                        {
                            var divElement2 = divElement.SelectSingleNode(".//div");
                            if (divElement2 != null)
                            {
                                var divElement3 = divElement2.SelectSingleNode(".//div");
                                if (divElement3 != null)
                                {
                                    var pElements = divElement3.SelectNodes(".//p");
                                    if (pElements != null)
                                    {
                                        foreach (var pElement in pElements)
                                        {
                                            if (Count <= 2)
                                            {
                                                var trimmedText = WebUtility.HtmlDecode(pElement.InnerText.Trim());

                                                // Remove the time part like '[2:00 PM]' or '[6:00 PM]' using regex
                                                trimmedText = System.Text.RegularExpressions.Regex.Replace(trimmedText, @"\s*\[\d{1,2}:\d{2} [AP]M\]\s*", "");

                                                var yearIndex = trimmedText.LastIndexOf("2023");
                                                if (yearIndex != -1 && yearIndex + 4 <= trimmedText.Length)
                                                {
                                                    var EventDeadline = trimmedText.Substring(0, yearIndex + 4).Trim();
                                                    var EventTitle = trimmedText.Substring(yearIndex + 4).Trim();

                                                    var AcademicCalendar = new UniversityCalendar
                                                    {
                                                        EventTitle = EventTitle,
                                                        EventDeadline = EventDeadline,
                                                        EventDetails = trimmedText,
                                                        EventNotification = "no",
                                                        UId = 3
                                                    };

                                                    UniversityCalendars.Add(AcademicCalendar);
                                                    Count++;
                                                }
                                            }
                                        }
                                    }
                                }
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
            var UniversityDepartments = _context.UniversityDepartment.Where(x => x.UId == 3).ToList();
            if(UniversityDepartments.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://kiet.edu.pk/programs/");
                var nodeElement = htmlDoc.DocumentNode.SelectNodes("//div[@class='bdt-fancy-list']");
                if (nodeElement != null)
                {
                    foreach (var node in nodeElement)
                    {
                        var ulElement = node.SelectSingleNode(".//ul");
                        if (ulElement != null)
                        {
                            var liElements = ulElement.SelectNodes(".//li");
                            if (liElements != null)
                            {
                                foreach (var liElement in liElements)
                                {
                                    var aElement = liElement.SelectSingleNode(".//a");
                                    if (aElement != null)
                                    {
                                        var divElement = aElement.SelectSingleNode(".//div");
                                        if (divElement != null)
                                        {
                                            var divElement2 = divElement.SelectSingleNode(".//div");
                                            if (divElement2 != null)
                                            {
                                                var h4Element = divElement2.SelectSingleNode(".//h4");
                                                if (h4Element != null)
                                                {
                                                    //var DepName = h4Element.InnerText?.Trim();
                                                    var DepName = WebUtility.HtmlDecode(h4Element.InnerText?.Trim());//decoding html elements such as - &*^%
                                                    var Department = new UniversityDepartments
                                                    {
                                                        DepartmentName = DepName,
                                                        Campus = "Karachi",
                                                        UId = 3
                                                    };
                                                    UniversityDepartments.Add(Department);
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
            var UniversityDocuments = _context.UniversityDocument.Where(x => x.UId == 3).ToList();
            if(UniversityDocuments.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://admissions.kiet.edu.pk/admission-process/");
                var nodeElement = htmlDoc.DocumentNode.SelectNodes("//div[@class='vc_row wpb_row vc_inner vc_row-fluid']");
                if (nodeElement != null)
                {
                    var node = nodeElement[1];
                   
                        
                    var divElement = node.SelectSingleNode(".//div");
                    if (divElement != null) {
                        var div2Element = divElement.SelectSingleNode(".//div");
                        if (div2Element != null)
                        {
                            var div3Element = div2Element.SelectSingleNode(".//div");
                            if (div3Element != null)
                            {

                                var div4Element = div3Element.SelectSingleNode(".//div");
                                if (div4Element != null)
                                {
                                    var div5Element = div4Element.SelectSingleNode(".//div");
                                    if (div5Element != null)
                                    {

                                        var pElement = div5Element.SelectSingleNode(".//p");
                                        if (pElement != null)
                                        {
                                            var bElements = pElement.SelectNodes(".//b");
                                            if (bElements != null)
                                            {
                                                foreach (var bElement in bElements)
                                                {
                                                    bElement.Remove();
                                                }
                                            }
                                                var pElementText = pElement.InnerText.Trim();
                                            var Document = new UniversityDocuments
                                            {
                                                DocumentRequirement = pElementText,
                                                UId = 3,
                                            };
                                            UniversityDocuments.Add(Document);
                                        }
                                    }
                                }
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
            var UniversityFees = _context.UniversityFee.Where(x => x.UId == 3).ToList();
            if(UniversityFees.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://admissions.kiet.edu.pk/general-fee-structure/");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='entry-content']");
                if (nodeElement != null)
                {
                    var h3Element = nodeElement.SelectSingleNode("//h3");
                    var tableElements = nodeElement.SelectNodes("//table");
                    var admissionFees = "";
                    var perCreditHr = "";
                    if (h3Element != null)
                    {
                         admissionFees = h3Element.InnerText.Trim();
                        admissionFees = ExtractAdmissionFee(admissionFees);
                    }
                    if(tableElements!= null)
                    {
                        var tableElement = tableElements[1];
                        if(tableElement!= null)
                        {
                            var tbody = tableElement.SelectSingleNode(".//tbody");
                            if(tbody!= null)
                            {
                                var trElements = tbody.SelectNodes(".//tr");
                                if(trElements!= null)
                                {
                                    var trElement = trElements[1];
                                    if(trElement!= null)
                                    {
                                        var tdElements = trElement.SelectNodes(".//td");
                                        if(tdElements!= null)
                                        {
                                             perCreditHr = tdElements[1].InnerText.Trim();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    var UniversityFee = new UniversityFee
                    {
                        AdmissionFee = admissionFees,
                        PerCreditHourFee = perCreditHr,
                        UId= 3
                    };
                    UniversityFees.Add(UniversityFee);
                }
                _unitOfWork.UniversityFeeRepository.AddRange(UniversityFees);
                _unitOfWork.UniversityFeeRepository.SaveChanges();
            }
            return UniversityFees;
        }
        private string ExtractAdmissionFee(string admissionFees)
        {
            // Split the string by '<br>' tag
            var parts = admissionFees.Split("<br>");

            // Iterate over the parts and find the one that contains 'Rs'
            foreach (var part in parts)
            {
                if (part.Contains("Rs"))
                {
                    // Extract the amount after 'Rs' and return
                    var startIndex = part.IndexOf("Rs") + 2; // Assuming the currency symbol is 2 characters
                    var endIndex = part.IndexOf("/-"); // Assuming the dash separates the amount and additional info

                    // If no dash is found, consider the end of the string
                    if (endIndex == -1)
                        endIndex = part.Length;

                    // Extract the amount and return
                    return part.Substring(startIndex, endIndex - startIndex).Trim();
                }
            }

            return ""; // If no amount is found, return an empty string
        }
    }
}
