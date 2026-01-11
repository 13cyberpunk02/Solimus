export interface BlogPost {
  id: number;
  title: string;
  excerpt: string;
  category: string;
  categoryColor: string;
  date: string;
  readTime: string;
  image?: string;
}

export interface TechCategory {
  name: string;
  icon: string;
  description: string;
  postsCount: number;
  gradient: string;
}