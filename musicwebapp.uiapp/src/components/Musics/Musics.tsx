import React, { useEffect, useState } from "react";
import {
  getAllMusicsAsync,
  uploadMusicAsync,
  likeMusicAsync,
  unlikeMusicAsync,
  getLikedMusicsAsync,
  getAllCommentsOfMusicAsync,
  commentOnMusicAsync,
} from "../../api/MusicServiceApi";
import "./Musics.css";
import { Music } from "../../types/Music";

const Musics: React.FC = () => {
  const [musics, setMusics] = useState<Music[]>([]);
  const [likedMusics, setLikedMusics] = useState<number[]>([]);
  const [comments, setComments] = useState<{ [musicId: number]: string[] }>({});
  const [newComment, setNewComment] = useState<string>("");

  const [formData, setFormData] = useState<{
    singerName: string;
    name: string;
    authorId: string;
    audioFile: File | null;
    imageFile: File | null;
  }>({
    singerName: "",
    name: "",
    authorId: "",
    audioFile: null,
    imageFile: null,
  });

  useEffect(() => {
    const fetchMusics = async () => {
      try {
        const fetchedMusics = await getAllMusicsAsync();
        setMusics(fetchedMusics);
        const liked = await getLikedMusicsAsync();
        setLikedMusics(liked); // Assuming the API returns an array of liked music objects

        // Fetching comments for all musics
        const commentsData: { [musicId: number]: string[] } = {};
        for (const music of fetchedMusics) {
          const musicComments = await getAllCommentsOfMusicAsync(music.id);
          commentsData[music.id] = musicComments;
        }
        setComments(commentsData);
      } catch (error) {
        console.error("Error fetching musics:", error);
      }
    };
    fetchMusics();
  }, []);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, files } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: files && files.length > 0 ? files[0] : value,
    }));
  };

  const handleUploadMusic = async (e: React.FormEvent) => {
    e.preventDefault();
    const { singerName, name, authorId, audioFile, imageFile } = formData;

    if (!audioFile || !imageFile) {
      alert("Please select both an audio file and an image file.");
      return;
    }

    try {
      await uploadMusicAsync({
        singerName,
        name,
        authorId,
        audioFile,
        imageFile,
      });
      alert("Music uploaded successfully!");
    } catch (error) {
      console.error("Error uploading music:", error);
      alert("Failed to upload music.");
    }
    const allMusics = await getAllMusicsAsync();
    setMusics(allMusics);
  };

  const handleLikeToggle = async (musicId: number) => {
    try {
      if (likedMusics.includes(musicId)) {
        await unlikeMusicAsync(musicId);
        setLikedMusics((prevLikedMusics) =>
          prevLikedMusics.filter((id) => id !== musicId)
        );
      } else {
        await likeMusicAsync(musicId);
        setLikedMusics((prevLikedMusics) => [...prevLikedMusics, musicId]);
      }
    } catch (error) {
      console.error("Error toggling like status:", error);
    }
  };

  const handleCommentChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewComment(e.target.value);
  };

  const handleCommentSubmit = async (musicId: number) => {
    if (!newComment) {
      alert("Please enter a comment.");
      return;
    }
    try {
      await commentOnMusicAsync(musicId, newComment);
      setNewComment(""); // Reset the input field
      // Update the comments state by adding the new comment
      setComments((prevComments) => {
        const updatedComments = { ...prevComments };
        updatedComments[musicId] = [
          ...(updatedComments[musicId] || []),
          newComment,
        ];
        return updatedComments;
      });
    } catch (error) {
      console.error("Error commenting on music:", error);
    }
  };

  return (
    <div className="musics-container">
      <h1>Musics</h1>
      <form className="upload-music-form" onSubmit={handleUploadMusic}>
        <input
          type="text"
          name="singerName"
          placeholder="Singer Name"
          value={formData.singerName}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="name"
          placeholder="Music Name"
          value={formData.name}
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="authorId"
          placeholder="Author Id"
          value={formData.authorId}
          onChange={handleInputChange}
        />
        <input
          type="file"
          name="audioFile"
          accept="audio/*"
          onChange={handleInputChange}
        />
        <input
          type="file"
          name="imageFile"
          accept="image/*"
          onChange={handleInputChange}
        />
        <button type="submit">Upload Music</button>
      </form>
      <div className="music-list">
        {musics.map((music) => (
          <div className="music-card" key={music.id}>
            <img src={music.imagePath || ""} alt={music.name || "Music"} />
            <h3>{music.name}</h3>
            <p>{music.singerName}</p>
            <audio controls>
              <source src={music.audioPath || ""} type="audio/mpeg" />
            </audio>
            <button
              className={`like-button ${
                likedMusics.includes(music.id) ? "liked" : ""
              }`}
              onClick={() => handleLikeToggle(music.id)}
            >
              {likedMusics.includes(music.id) ? "Unlike" : "Like"}
            </button>

            {/* Comment Section */}
            <div className="comment-section">
              <input
                type="text"
                value={newComment}
                onChange={handleCommentChange}
                placeholder="Add a comment..."
              />
              <button onClick={() => handleCommentSubmit(music.id)}>
                Comment
              </button>
              <div className="comments-list">
                {comments[music.id]?.map((comment, index) => (
                  <div key={index} className="comment-item">
                    {comment}
                  </div>
                ))}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Musics;
