import { PostTag } from "./PostTag";
import { Rating } from "./Rating";

export class Post {
    id: number;
    title: string;
    content: Text;
    user_id: number;
    ratings: Rating[];
    postTags: PostTag[];
    created_At: Date;
    updated_at: Date;
}