import axios from "axios";
import { LoginDto } from "../types/LoginDto";

const apiGatewayUrl = "http://localhost:5001";
const registerApi = apiGatewayUrl + "/r";
const loginApi = apiGatewayUrl + "/l";

const registerAsync = async (registerDto: FormData): Promise<void> => {
  try {
    const result = await axios.post(registerApi, registerDto, {
      // No need to set the Content-Type header, axios will set it automatically
    });
    console.log(result.data); // Log the response from the server
  } catch (error) {
    console.error("Error during registration:", error);
  }
};

const loginAsync = async (loginDto: LoginDto): Promise<void> => {
  const result = await axios.post(loginApi, loginDto);
  console.log(result.data);
  localStorage.setItem("token", result.data.token);
  localStorage.setItem("userId", result.data.loggedUser.id);
};

export { registerAsync, loginAsync };
