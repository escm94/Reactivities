// we won't prefix with "I" like we do in C# since this is TS, but we could
export interface Activity {
  id: string;
  title: string;
  date: Date | null;
  description: string;
  category: string;
  city: string;
  venue: string;
}
