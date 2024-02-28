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
    public class BahriaService : IBahriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public BahriaService(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public List<UniversityCalendar> GetUniversityCalendars()
        {
            var UniversityCalendars = _context.UniversityCalendar.Where(x => x.UId == 5).ToList();
            if (UniversityCalendars.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://bahria.edu.pk/admissions/#");
                var masternodeElement = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='impDateKch']");
                var EventTitle = "";
                var EventDeadline = "";
                var EventDetails = "";

                if (masternodeElement != null)
                {
                    var tableElement = masternodeElement.SelectSingleNode(".//table");
                    if (tableElement != null)
                    {
                        var tbodyElement = tableElement.SelectSingleNode(".//tbody");
                        if (tbodyElement != null)
                        {
                            var trElements = tbodyElement.SelectNodes(".//tr");
                            if (trElements != null && trElements.Count > 1) // Ensure there is at least one tr element
                            {
                                for (int i = 1; i < trElements.Count; i++) // Start from index 1 to skip the first tr element
                                {
                                    var trElement = trElements[i];
                                    var tdElements = trElement.SelectNodes(".//td");
                                    if (tdElements != null)
                                    {
                                        EventTitle = WebUtility.HtmlDecode(tdElements[1].InnerText.Trim());
                                        EventDeadline = WebUtility.HtmlDecode(tdElements[2].InnerText.Trim());
                                        EventDetails = WebUtility.HtmlDecode(tdElements[4].InnerText.Trim());

                                        var UniversityCalendar = new UniversityCalendar
                                        {
                                            EventTitle = EventTitle,
                                            EventDeadline = EventDeadline,
                                            EventDetails = EventDetails,
                                            EventNotification = "no",
                                            UId = 5
                                        };

                                        UniversityCalendars.Add(UniversityCalendar);
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
            var UniversityDepartments = _context.UniversityDepartment.Where(x => x.UId == 5).ToList();
            if (UniversityDepartments.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://www.bahria.edu.pk/index.php/academics/under-graduate-programs/");
                //Scrape data from nodeElement
                var nodeElement = htmlDoc.DocumentNode.SelectNodes("//table[@class='table table-bordered table-striped']");
                if (nodeElement != null)
                {
                    foreach (var node in nodeElement)
                    {
                        var tbodynode = node.SelectSingleNode(".//tbody");
                        if (tbodynode != null)
                        {
                            var trnodes = tbodynode.SelectNodes(".//tr");

                            if (trnodes != null)
                            {
                                foreach (var trnode in trnodes)
                                {
                                    var tdnodes = trnode.SelectNodes(".//td");
                                    if (tdnodes != null)
                                    {
                                        string tdname = (tdnodes.Count > 3) ? tdnodes[3].InnerText.Trim() : (tdnodes.Count > 2) ? tdnodes[2].InnerText.Trim() : string.Empty;

                                        foreach (var tdnode in tdnodes)
                                        {
                                            var coursenode = tdnode.SelectSingleNode(".//a");
                                            if (coursenode != null)
                                            {
                                                var depName = WebUtility.HtmlDecode( coursenode.InnerText?.Trim());

                                                if (!string.IsNullOrWhiteSpace(depName))
                                                {
                                                    //Modify the properties of UniversityCourses based on the data from nodeElement
                                                    var UniversityDepartment = new UniversityDepartments
                                                    {
                                                        DepartmentName = depName,
                                                        Campus = tdname,
                                                        UId = 5
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

                //Save the scraped data to the database
                _unitOfWork.UniversityDepartmentRepository.AddRange(UniversityDepartments);
                _unitOfWork.UniversityDepartmentRepository.SaveChanges();

            }
            return UniversityDepartments;
        }
        public List<UniversityDocuments> GetUniversityDocuments()
        {
            var UniversityDocuments = _context.UniversityDocument.Where(x=>x.UId==5).ToList();
            //importantdocuments
            if (UniversityDocuments.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://www.bahria.edu.pk/index.php/documents-required-ug/");
                var nodeElement2 = htmlDoc.DocumentNode.SelectNodes("//div[@class='description']");

                if (nodeElement2 != null)
                {
                    foreach (var node in nodeElement2)
                    {
                        var ulNode = node.SelectSingleNode(".//ul");
                        if (ulNode != null)
                        {
                            var liNodes = ulNode.SelectNodes(".//li");
                            if (liNodes != null)
                            {
                                foreach (var liNode in liNodes)
                                {
                                    var liText = liNode.InnerText.Trim();
                                    var documents = new UniversityDocuments
                                    {
                                        DocumentRequirement = liText,
                                        UId=5
                                    };
                                    UniversityDocuments.Add(documents);
                                }
                            }
                        }
                    }
                    // Save the scraped data to the database
                    _context.UniversityDocument.AddRange(UniversityDocuments);
                    _context.SaveChanges();
                }
            }
            return UniversityDocuments;
        }

        public List<UniversityFee> GetUniversityFee()
        {
            var UniversityFees = _context.UniversityFee.Where(x => x.UId == 5).ToList();
            if(UniversityFees.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://www.bahria.edu.pk/index.php/under-graduate-programs-fee/");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-bordered table-striped']");
                if (nodeElement != null)
                {
                    var tbodyElement = nodeElement.SelectSingleNode("//tbody");
                    if (tbodyElement != null)
                    {
                        var trElements = tbodyElement.SelectNodes("//tr");
                        if (trElements != null)
                        {
                            var creditHourElement = trElements[4];
                            var admissionFeeElement = trElements[7];
                            var tdElements = creditHourElement.SelectNodes("//td");
                            var admissiontdElements = admissionFeeElement.SelectNodes("//td");
                            var admissionFees = "";
                            var creditHourFees = "";
                            if (tdElements != null)
                            {
                                creditHourFees = tdElements[79].InnerText.Trim();
                            }
                            if (admissiontdElements != null)
                            {
                                admissionFees = admissiontdElements[151].InnerText.Trim();
                            }

                            var FeeDetail = new UniversityFee
                            {
                                AdmissionFee = admissionFees,
                                PerCreditHourFee = creditHourFees,
                                UId = 5
                            };
                            UniversityFees.Add(FeeDetail);
                        }
                        _unitOfWork.UniversityFeeRepository.AddRange(UniversityFees);
                        _unitOfWork.UniversityFeeRepository.SaveChanges();
                    }
                }
            }
            return UniversityFees;
        }
    }
}
