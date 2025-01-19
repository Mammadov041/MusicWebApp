import { Playlist } from "./Playlist";

export type Music = {
  id: number;
  singerName?: string | null;
  name?: string | null;
  imagePath?: string | null;
  audioPath?: string | null;
  authorId: string;
  playlists?: Playlist[] | null;
};
