import { useEffect, useState } from 'react'
import axios from "axios"
import './App.css'

const API = "https://localhost:7100/api/UniversityFilter"

function App() {
  const [data, setData] = useState([])
  const [error,setError] = useState("")
  const [selectedDepartment, setSelectedDepartment] = useState("");
  const [selectedCampus, setSelectedCampus] = useState("");
  const [selectedFee, setSelectedFee] = useState("");
  const [toggle,setToggle]=useState(true)

  const toggleSidebar = () => {
    setToggle(!toggle);
  };

  const getData = async () => {
    try {
      let url = API;
      const params = {};

      if (selectedDepartment) {
        params.departmentName = selectedDepartment;
      }

      if (selectedCampus) {
        params.campus = selectedCampus;
      }

      if (selectedFee) {
        params.fee = selectedFee;
      }

      const queryString = new URLSearchParams(params).toString();

      if (queryString) {
        url = `${API}?${queryString}`;
      }

      const res = await axios.get(url);
      setData(res.data);
    } catch (error) {
      setError(error.message);
    }
  };

  const handleRadioChange = (event) => {
    const departmentName = event.target.value;
    setSelectedDepartment(departmentName);
  };

  const handleCampusChange = (event) => {
    const campus = event.target.value;
    setSelectedCampus(campus);
  };

  const handleFeeChange = (event) => {
    const fee = event.target.value;
    setSelectedFee(fee);
  };

  useEffect(() => {
    getData();
  }, [selectedDepartment, selectedCampus, selectedFee]);

  return (
    <>
     <button onClick={toggleSidebar} className="toggle-button">
     {toggle ? 'Close' : 'Open'} Sidebar
     </button>
     {error!==""&& <h2>{error}</h2> }
     <div className='container'>
   
     <div className={toggle ? 'sidebar' : 'sidebar-closed'}>
    
  
          <div className="department-container">
          <h3>DEPARTMENTS</h3>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Computer Science"
            checked={selectedDepartment === "Computer Science"}
            onChange={handleRadioChange}
          />
          Computer Science
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Accounting"
            checked={selectedDepartment === "Accounting"}
            onChange={handleRadioChange}
          />
          Accounting
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Economics"
            checked={selectedDepartment === "Economics"}
            onChange={handleRadioChange}
          />
          Economics
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Business"
            checked={selectedDepartment === "Business"}
            onChange={handleRadioChange}
          />
          Business
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Supply Chain Management"
            checked={selectedDepartment === "Supply Chain Management"}
            onChange={handleRadioChange}
          />
          Supply Chain Management
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Electrical Engineering"
            checked={selectedDepartment === "Electrical Engineering"}
            onChange={handleRadioChange}
          />
          Electrical Engineering
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Robotics"
            checked={selectedDepartment === "Robotics"}
            onChange={handleRadioChange}
          />
          Robotics
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Software Engineering"
            checked={selectedDepartment === "Software Engineering"}
            onChange={handleRadioChange}
          />
          Software Engineering
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Computer Engineering"
            checked={selectedDepartment === "Computer Engineering"}
            onChange={handleRadioChange}
          />
          Computer Engineering
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Artificial Intelligence"
            checked={selectedDepartment === "Artificial Intelligence"}
            onChange={handleRadioChange}
          />
          Artificial Intelligence
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Information Technology"
            checked={selectedDepartment === "Information Technology"}
            onChange={handleRadioChange}
          />
          Information Technology
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Environmental Sciences"
            checked={selectedDepartment === "Environmental Sciences"}
            onChange={handleRadioChange}
          />
          Environmental Sciences
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Geophysics"
            checked={selectedDepartment === "Geophysics"}
            onChange={handleRadioChange}
          />
          Geophysics
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Geology"
            checked={selectedDepartment === "Geology"}
            onChange={handleRadioChange}
          />
          Geology
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Psychology"
            checked={selectedDepartment === "Psychology"}
            onChange={handleRadioChange}
          />
          Psychology
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Social Sciences"
            checked={selectedDepartment === "Social Sciences"}
            onChange={handleRadioChange}
          />
          Social Sciences
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Public Health"
            checked={selectedDepartment === "Public Health"}
            onChange={handleRadioChange}
          />
          Public Health
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Mathematics"
            checked={selectedDepartment === "Mathematics"}
            onChange={handleRadioChange}
          />
          Mathematics
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Marine Sciences"
            checked={selectedDepartment === "Marine Sciences"}
            onChange={handleRadioChange}
          />
          Marine Sciences
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Biotechnology"
            checked={selectedDepartment === "Biotechnology"}
            onChange={handleRadioChange}
          />
          Biotechnology
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Media"
            checked={selectedDepartment === "Media"}
            onChange={handleRadioChange}
          />
          Media
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Islamic Studies"
            checked={selectedDepartment === "Islamic Studies"}
            onChange={handleRadioChange}
          />
          Islamic Studies
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Bioinformatics"
            checked={selectedDepartment === "Bioinformatics"}
            onChange={handleRadioChange}
          />
          Bioinformatics
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Biosciences"
            checked={selectedDepartment === "Biosciences"}
            onChange={handleRadioChange}
          />
          Biosciences
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Chemical Engineering"
            checked={selectedDepartment === "Chemical Engineering"}
            onChange={handleRadioChange}
          />
          Chemical Engineering
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Civil Engineering"
            checked={selectedDepartment === "Civil Engineering"}
            onChange={handleRadioChange}
          />
          Civil Engineering
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Cyber Security"
            checked={selectedDepartment === "Cyber Security"}
            onChange={handleRadioChange}
          />
          Cyber Security
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Development Studies"
            checked={selectedDepartment === "Development Studies"}
            onChange={handleRadioChange}
          />
          Development Studies
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Electronics"
            checked={selectedDepartment === "Electronics"}
            onChange={handleRadioChange}
          />
          Electronics
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Food Sciences"
            checked={selectedDepartment === "Food Sciences"}
            onChange={handleRadioChange}
          />
          Food Sciences
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="International Relations"
            checked={selectedDepartment === "International Relations"}
            onChange={handleRadioChange}
          />
          International Relations
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Mechanical Engineering"
            checked={selectedDepartment === "Mechanical Engineering"}
            onChange={handleRadioChange}
          />
          Mechanical Engineering
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Physics"
            checked={selectedDepartment === "Physics"}
            onChange={handleRadioChange}
          />
          Physics
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Statistics"
            checked={selectedDepartment === "Statistics"}
            onChange={handleRadioChange}
          />
          Statistics
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Data Science"
            checked={selectedDepartment === "Data Science"}
            onChange={handleRadioChange}
          />
          Data Science
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Financial Technologies"
            checked={selectedDepartment === "Financial Technologies"}
            onChange={handleRadioChange}
          />
          Financial Technologies
        </label>
        </div>
        <div className="label-container">
     <label>
          <input
            type="radio"
            value="Digital Marketing"
            checked={selectedDepartment === "Digital Marketing"}
            onChange={handleRadioChange}
          />
          Digital Marketing
        </label>
        </div>
        </div>
     

     
          <div className="campus-container">
         <h3>CAMPUSES</h3>
         <div className="label-container">
         <label>
          <input
            type="radio"
            value="Karachi"
            checked={selectedCampus === "Karachi"}
            onChange={handleCampusChange}
          />
          Karachi
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Lahore"
            checked={selectedCampus === "Lahore"}
            onChange={handleCampusChange}
          />
          Lahore
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Islamabad"
            checked={selectedCampus === "Islamabad"}
            onChange={handleCampusChange}
          />
          Islamabad
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Wah"
            checked={selectedCampus === "Wah"}
            onChange={handleCampusChange}
          />
          Wah
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Attock"
            checked={selectedCampus === "Attock"}
            onChange={handleCampusChange}
          />
          Attock
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Sahiwal"
            checked={selectedCampus === "Sahiwal"}
            onChange={handleCampusChange}
          />
          Sahiwal
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Vehari"
            checked={selectedCampus === "Vehari"}
            onChange={handleCampusChange}
          />
          Vehari
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Abbottabad"
            checked={selectedCampus === "Abbottabad"}
            onChange={handleCampusChange}
          />
          Abbottabad
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Peshawar"
            checked={selectedCampus === "Peshawar"}
            onChange={handleCampusChange}
          />
          Peshawar
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Faisalabad"
            checked={selectedCampus === "Faisalabad"}
            onChange={handleCampusChange}
          />
          Faisalabad
        </label>
        </div>
        <div className="label-container">
        <label>
          <input
            type="radio"
            value="Chiniot"
            checked={selectedCampus === "Chiniot"}
            onChange={handleCampusChange}
          />
          Chiniot
        </label>
        </div>
        </div>
       
       
        </div>

      <div className="table-container"> 
        <h1>University Filtration</h1>
        <table>
          <thead>
            <tr>
              <td className='td' id='UniNameHeading'>University Name</td>
            </tr>
          </thead>
          <tbody>
            {data.map((university) => (
              <tr key={university.id} className='table-row'>
                <td className='td' id='uniId'>{university.id}</td>
                <td className='td' id='uniNameTd'>{university.universityName}</td>  
              </tr>
            ))}
          </tbody>
        </table>
      </div>
     </div>
    </>
  )
}

export default App
