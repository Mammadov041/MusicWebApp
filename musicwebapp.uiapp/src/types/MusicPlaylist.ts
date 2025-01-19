import { Music } from "./Music";
import { Playlist } from "./Playlist";

export type MusicPlaylist = {
  id: number;
  musicId: number;
  playlistId: number;
  music: Music;
  playlist: Playlist;
};
