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
    public class FastService : IFastService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public FastService(IUnitOfWork unitOfWork,ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public List<UniversityCalendar> GetUniversityCalendars()
        {
            var UniversityCalendars = _context.UniversityCalendar.Where(x=>x.UId == 1).ToList();
            if(UniversityCalendars.Count == 0)
            {

                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://www.nu.edu.pk/admissions/Schedule");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//tbody[@class='table-body']");
                var EventName = "";
                var EventDeadline = "";
                if (nodeElement != null)
                {
                    var trElements = nodeElement.SelectNodes("//tr");
                    if (trElements != null)
                    {
                        foreach (var trElement in trElements)
                        {
                            var tdElements = trElement.SelectNodes(".//td");//selects from current element of loop 

                            if (tdElements != null)
                            {
                                EventName = WebUtility.HtmlDecode(tdElements[0].InnerText.Trim());
                                EventDeadline = WebUtility.HtmlDecode(tdElements[1].InnerText.Trim());

                                var UniversityCalendar = new UniversityCalendar
                                {
                                    EventTitle = EventName,
                                    EventDetails = "No Information Avaialable",
                                    EventDeadline = EventDeadline,
                                    EventNotification = "Notification",
                                    UId = 1,
                                };
                                UniversityCalendars.Add(UniversityCalendar);


                            }
                        }
                    }
                }
                //_context.UniversityCalendar.AddRange(UniversityCalendars);
                //_context.SaveChanges();
                _unitOfWork.UniversityCalendarRepository.AddRange(UniversityCalendars);
                _unitOfWork.UniversityCalendarRepository.SaveChanges();
            }
            return UniversityCalendars;
        }

        public List<UniversityDepartments> GetUniversityDepartments()
        {
            var UniversityDepartments = _context.UniversityDepartment.Where(x => x.UId == 1).ToList();
            if(UniversityDepartments.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://www.nu.edu.pk/Degree-Programs");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//tbody[@class='table-body']");

                if (nodeElement != null)
                {
                    var tbodyChildren = nodeElement.SelectNodes(".//tr");

                    if (tbodyChildren != null)
                    {
                        foreach (var tbodyChild in tbodyChildren)
                        {
                            var tdNodes = tbodyChild.SelectNodes(".//td");
                            var campusNamesList = new List<string>();

                            if (tdNodes != null)
                            {
                                for (int i = 0; i < tdNodes.Count; i++)
                                {
                                    var tdNode = tdNodes[i];

                                    // Check for td with class 'text-center'
                                    if (tdNode.Attributes["class"]?.Value == "text-center")
                                    {
                                        var aTag = tdNode.SelectSingleNode(".//a");

                                        if (aTag != null)
                                        {
                                            // Add campus name based on the index of the 'td'
                                            if (i == 1)
                                            {
                                                campusNamesList.Add("Chiniot-Faisalabad");
                                            }
                                            else if (i == 2)
                                            {
                                                campusNamesList.Add("Islamabad");
                                            }
                                            else if (i == 3)
                                            {
                                                campusNamesList.Add("Karachi");
                                            }
                                            else if (i == 4)
                                            {
                                                campusNamesList.Add("Lahore");
                                            }
                                            else if (i == 5)
                                            {
                                                campusNamesList.Add("Peshawar");
                                            }
                                        }
                                    }
                                }
                            }

                            foreach (var tdNode in tdNodes)
                            {
                                var Courses = tdNode.SelectSingleNode(".//a");

                                if (Courses == null)
                                {
                                    continue;
                                }

                                var DepartmentName = WebUtility.HtmlDecode(Courses.InnerText?.Trim());

                                if (!string.IsNullOrWhiteSpace(DepartmentName))
                                {
                                    var Department = new UniversityDepartments
                                    {
                                        DepartmentName = DepartmentName,
                                        UId = 1
                                    };

                                    // Add the list of campus names to the overall list
                                    Department.Campus = string.Join(",", campusNamesList);

                                    UniversityDepartments.Add(Department);
                                }
                            }
                        }
                    }

                    if (UniversityDepartments.Count > 0)
                    {
                        _unitOfWork.UniversityDepartmentRepository.AddRange(UniversityDepartments);
                        _unitOfWork.UniversityDepartmentRepository.SaveChanges();
                    }

                }

            }
            return UniversityDepartments;
        }


        public List<UniversityFee> GetUniversityFee()
        {
            var UniversityFees = _context.UniversityFee.Where(x=>x.UId == 1).ToList();
            if(UniversityFees.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://www.nu.edu.pk/Admissions/FeeStructure");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//p[@class='text-justify']");
                var tableElement = htmlDoc.DocumentNode.SelectSingleNode("//tbody[@class='table-body']");
                var creditHourFee = "";
                if (nodeElement != null && tableElement != null)
                {
                    var trElement = tableElement.SelectSingleNode("//tr");
                    if (trElement != null)
                    {
                        var tdElement = trElement.SelectNodes("//td");
                        if (tdElement != null)
                        {
                            creditHourFee = tdElement[1].InnerText.Trim();
                        }
                    }
                    var admissionFeeText = nodeElement.InnerText.Trim();
                    var admissionFee = " ";
                    // Use the regular expression to extract the RS amount
                    string pattern = @"Rs\s+\d+,\d+"; // Adjust the pattern based on the actual format
                    var match = Regex.Match(admissionFeeText, pattern);

                    if (match.Success)
                    {
                        admissionFee = match.Value;
                        // Now, you can use the 'admissionFee' value as needed.


                    }
                    var FeeDetail = new UniversityFee
                    {
                        AdmissionFee = admissionFee,
                        PerCreditHourFee = creditHourFee,
                        UId = 1
                    };
                    UniversityFees.Add(FeeDetail);

                   _unitOfWork.UniversityFeeRepository.AddRange(UniversityFees);
                    _unitOfWork.UniversityFeeRepository.SaveChanges();
                }
            }
            return UniversityFees;
        }

        public List<UniversityDocuments> GetUniversityDocuments()
        {
            var UniversityDocuments = _context.UniversityDocument.Where(x=>x.UId == 1).ToList();
            if (UniversityDocuments.Count == 0)
            {
                var UniversityDocument = new UniversityDocuments
                {
                    DocumentRequirement = "No data available",
                    UId = 1,
                };
                UniversityDocuments.Add(UniversityDocument);
                _unitOfWork.UniversityDocumentRepository.AddRange(UniversityDocuments);
                _unitOfWork.UniversityDocumentRepository.SaveChanges();
            }
            return UniversityDocuments;
        }

        
    }
}
