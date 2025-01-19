import { Music } from "./Music";

export type Playlist = {
  id: number; // Corresponds to C# 'int Id'
  name?: string | null; // Corresponds to C# 'string? Name'
  userId: string; // Corresponds to C# 'string UserId'
  musics?: Music[] | null;
};
