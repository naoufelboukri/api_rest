import { Post } from "./Post";
import { Rating } from "./Rating";

export class User {
    id: number;
    username: string;
    password: string;
    role: string;
    created_at: Date;
    updated_at: string;
    ratings: Rating[];
    posts: Post[];
}