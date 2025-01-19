import React, { useState } from "react";
import "./AuthForms.css";
import { registerAsync } from "../api/IdentityServiceApi";

const RegisterForm: React.FC = () => {
  const [formData, setFormData] = useState({
    username: "",
    password: "",
    email: "",
    file: null as File | null, // Properly typed for file
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, files } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: files && files.length > 0 ? files[0] : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    console.log("Register Data:", formData);

    // Create a FormData object to send the file and other data
    const form = new FormData();
    form.append("username", formData.username);
    form.append("password", formData.password);
    form.append("email", formData.email);

    if (formData.file) {
      form.append("file", formData.file); // Add the file to the form data
    }

    // Call the registerAsync function (which should handle the FormData submission)
    await registerAsync(form);
  };

  return (
    <form className="auth-form" onSubmit={handleSubmit}>
      <h2>Register</h2>
      <div className="form-group">
        <label htmlFor="username">Username</label>
        <input
          type="text"
          id="username"
          name="username"
          value={formData.username}
          onChange={handleChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="password">Password</label>
        <input
          type="password"
          id="password"
          name="password"
          value={formData.password}
          onChange={handleChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="email">Email</label>
        <input
          type="email"
          id="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
          required
        />
      </div>
      <div className="form-group">
        <label htmlFor="image">Profile Image</label>
        <input
          type="file"
          id="image"
          name="file" // Ensure this name matches with the server-side form parameter
          onChange={handleChange}
          accept="image/*"
          required
        />
      </div>
      <button type="submit" className="submit-button">
        Register
      </button>
    </form>
  );
};

export default RegisterForm;
