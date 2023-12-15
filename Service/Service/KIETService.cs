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
    public class KIETService:IKIETService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public KIETService(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public List<UniversityCalendar> GetUniversityCalendars()
        {
            var UniversityCalendars = _context.UniversityCalendar.Where(x => x.UId == 3).ToList();
            if(UniversityCalendars.Count == 0 )
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://admissions.kiet.edu.pk/admission-schedule/");
                var masternodeElement = htmlDoc.DocumentNode.SelectNodes("//div[@class='wpb_wrapper']");
                var EventTitle = "";
                var EventDeadline = "";
                var EventDetails = "";
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
                                                EventDetails = WebUtility.HtmlDecode(pElement.InnerText.Trim());


                                                var AcademicCalendar = new UniversityCalendar
                                                {
                                                    EventTitle = EventTitle,
                                                    EventDeadline = EventDeadline,
                                                    EventDetails = EventDetails,
                                                    EventNotification = "no",
                                                    UId = 3
                                                };
                                                Count++;
                                                UniversityCalendars.Add(AcademicCalendar);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        _unitOfWork.UniversityCalendarRepository.AddRange(UniversityCalendars);
                        _unitOfWork.UniversityCalendarRepository.SaveChanges();
                    }
                }
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
            throw new NotImplementedException();
        }

        public List<UniversityFee> GetUniversityFee()
        {
            var UniversityFees = _context.UniversityFee.Where(x => x.UId == 3).ToList();
            if(UniversityFees.Count == 0)
            {
                var web = new HtmlWeb();
                var htmlDoc = web.Load("https://admissions.kiet.edu.pk/general-fee-structure-detailed/");
                var nodeElement = htmlDoc.DocumentNode.SelectSingleNode("//tr[@class='row-14 even']");
                if (nodeElement != null)
                {
                    var tdElements = nodeElement.SelectNodes("//td");
                    if (tdElements != null)
                    {
                        var credithrFees = tdElements[98].InnerText.Trim();
                        var admissionFees = tdElements[100].InnerText.Trim();

                        var FeeDetail = new UniversityFee
                        {
                            AdmissionFee = admissionFees,
                            PerCreditHourFee = credithrFees,
                            UId = 3
                        };
                        UniversityFees.Add(FeeDetail);
                    }
                }
                _unitOfWork.UniversityFeeRepository.AddRange(UniversityFees);
                _unitOfWork.UniversityFeeRepository.SaveChanges();
            }
            return UniversityFees;
        }
    }
}
