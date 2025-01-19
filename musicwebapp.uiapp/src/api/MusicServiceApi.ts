import axios from "axios";
import { MusicDto } from "../types/MusicDto";
import { Music } from "../types/Music";

const apiGatewayUrl = "http://localhost:5001";
const uploadMusicApi = `${apiGatewayUrl}/um`; // Endpoint for uploading music
const allMusicsApi = `${apiGatewayUrl}/am`; // Endpoint for fetching all musics
const likedMusicsApi = `${apiGatewayUrl}/lm`; // Endpoint for fetching liked musics
const likeMusicApi = `${apiGatewayUrl}/lm`; // Endpoint for liking a music
const unlikeMusicApi = `${apiGatewayUrl}/unm`; // Endpoint for unliking a music
const commentMusicApi = `${apiGatewayUrl}/cm`; // Endpoint for commenting on a music
const musicCommentsApi = `${apiGatewayUrl}/mc`; // Endpoint for fetching comments on a music

const getAllMusicsAsync = async (): Promise<Music[]> => {
  const token = localStorage.getItem("token");
  try {
    const response = await axios.get(allMusicsApi, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching all musics:", error);
    throw error;
  }
};

const uploadMusicAsync = async (musicDto: MusicDto) => {
  const token = localStorage.getItem("token");
  const formData = new FormData();

  formData.append("singerName", musicDto.singerName || "");
  formData.append("name", musicDto.name || "");
  formData.append("authorId", musicDto.authorId);

  // Only append files if they exist
  if (musicDto.audioFile) {
    formData.append("audioFile", musicDto.audioFile);
  }
  if (musicDto.imageFile) {
    formData.append("imageFile", musicDto.imageFile);
  }

  try {
    const response = await axios.post(uploadMusicApi, formData, {
      headers: {
        "Content-Type": "multipart/form-data",
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error uploading music:", error);
    throw error;
  }
};

const getLikedMusicsAsync = async (): Promise<number[]> => {
  const token = localStorage.getItem("token");
  const userId = localStorage.getItem("userId");
  try {
    const response = await axios.get(likedMusicsApi + `?userId=${userId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    console.log("Users liked musics:", response.data);
    return response.data;
  } catch (error) {
    console.error("Error fetching all musics:", error);
    throw error;
  }
};

const likeMusicAsync = async (musicId: number) => {
  const token = localStorage.getItem("token");
  const userId = localStorage.getItem("userId");
  try {
    const response = await axios.post(
      likeMusicApi + `?userId=${userId}&musicId=${musicId}`,
      null,
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );
    return response.data;
  } catch (error) {
    console.error("Error liking music:", error);
    throw error;
  }
};

const unlikeMusicAsync = async (musicId: number) => {
  const token = localStorage.getItem("token");
  const userId = localStorage.getItem("userId");
  try {
    const response = await axios.post(
      unlikeMusicApi + `?userId=${userId}&musicId=${musicId}`,
      null,
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );
    return response.data;
  } catch (error) {
    console.error("Error unliking music:", error);
    throw error;
  }
};

const getAllCommentsOfMusicAsync = async (musicId: number) => {
  const token = localStorage.getItem("token");
  try {
    const response = await axios.get(musicCommentsApi + `?musicId=${musicId}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching comments of music:", error);
    throw error;
  }
};

const commentOnMusicAsync = async (musicId: number, comment: string) => {
  const token = localStorage.getItem("token");
  try {
    const response = await axios.post(
      commentMusicApi,
      { musicId, comment }, // Pass data in the request body
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json", // Set content type
        },
      }
    );
    return response.data;
  } catch (error) {
    console.error("Error commenting on music:", error);
    throw error;
  }
};

export {
  getAllMusicsAsync,
  uploadMusicAsync,
  getLikedMusicsAsync,
  likeMusicAsync,
  unlikeMusicAsync,
  getAllCommentsOfMusicAsync,
  commentOnMusicAsync,
};
