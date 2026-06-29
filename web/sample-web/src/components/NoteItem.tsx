import { useState } from 'react';
import { type NoteResponse } from '../services/notesApi';
import { useDeleteNote, useUpdateNote } from '../hooks/useNotes';
import { NoteForm } from './NoteForm';

interface NoteItemProps {
  note: NoteResponse;
}

export function NoteItem({ note }: NoteItemProps) {
  const [editing, setEditing] = useState(false);
  const updateNote = useUpdateNote();
  const deleteNote = useDeleteNote();

  function handleUpdate(data: { title: string; content: string }) {
    updateNote.mutate(
      { id: note.id, data },
      { onSuccess: () => setEditing(false) },
    );
  }

  return (
    <article className="rounded-xl border border-gray-200 bg-white p-5 shadow-sm hover:shadow-md transition-shadow">
      {editing ? (
        <NoteForm
          initialTitle={note.title}
          initialContent={note.content}
          submitLabel="Update"
          onSubmit={handleUpdate}
          onCancel={() => setEditing(false)}
          isPending={updateNote.isPending}
        />
      ) : (
        <>
          <div className="flex items-start justify-between gap-2">
            <h2 className="text-base font-semibold text-gray-900 leading-snug">
              {note.title}
            </h2>
            <div className="flex gap-2 shrink-0">
              <button
                onClick={() => setEditing(true)}
                className="text-xs text-blue-600 hover:text-blue-800 font-medium transition-colors"
              >
                Edit
              </button>
              <button
                onClick={() => deleteNote.mutate(note.id)}
                disabled={deleteNote.isPending}
                className="text-xs text-red-500 hover:text-red-700 font-medium disabled:opacity-50 transition-colors"
              >
                Delete
              </button>
            </div>
          </div>
          <p className="mt-2 text-sm text-gray-600 whitespace-pre-wrap leading-relaxed">
            {note.content}
          </p>
          <p className="mt-3 text-xs text-gray-400">
            {new Date(note.createdAt).toLocaleString()}
          </p>
        </>
      )}
    </article>
  );
}
