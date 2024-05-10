import React, { useState } from 'react';
import './App.css'; // Import your CSS file

function SidebarClosed({ openSidebar }) {
  const [isOpen, setIsOpen] = useState(false);

  const toggleSidebar = () => {
    setIsOpen(!isOpen);
    openSidebar(); // Call the openSidebar function passed from the parent component
  };

  return (
    <div className={isOpen ? 'sidebar' : 'sidebar-closed'}>
      {/* Button to toggle the sidebar */}
      <button onClick={toggleSidebar}>Toggle Sidebar</button>
    </div>
  );
}

export default SidebarClosed;
