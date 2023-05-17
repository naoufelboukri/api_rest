import { PostTag } from "./PostTag";
import { Rating } from "./Rating";

export class Post {
    id: number;
    title: string;
    content: Text;
    user_id: number;
    ratings: Rating[];
    PostTags: PostTag[];
    created_at: Date;
    updated_at: Date;
}