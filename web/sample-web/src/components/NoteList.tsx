import { type NoteResponse } from '../services/notesApi';
import { NoteItem } from './NoteItem';

interface NoteListProps {
  notes: NoteResponse[];
}

export function NoteList({ notes }: NoteListProps) {
  if (notes.length === 0) {
    return (
      <p className="text-center text-gray-400 py-12 text-sm">
        No notes yet. Create one above!
      </p>
    );
  }

  return (
    <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
      {notes.map((note) => (
        <NoteItem key={note.id} note={note} />
      ))}
    </div>
  );
}
