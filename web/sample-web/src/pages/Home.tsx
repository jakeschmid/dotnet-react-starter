import { useCreateNote, useNotes } from '../hooks/useNotes';
import { NoteForm } from '../components/NoteForm';
import { NoteList } from '../components/NoteList';

export function Home() {
  const { data: notes, isLoading, isError } = useNotes();
  const createNote = useCreateNote();

  return (
    <div className="min-h-screen bg-gray-50">
      <header className="bg-white border-b border-gray-200 px-4 py-5">
        <div className="mx-auto max-w-4xl">
          <h1 className="text-2xl font-bold text-gray-900">📝 Notes</h1>
          <p className="mt-1 text-sm text-gray-500">
            A simple notes app — React + ASP.NET Core starter
          </p>
        </div>
      </header>

      <main className="mx-auto max-w-4xl px-4 py-8 space-y-8">
        {/* Create note form */}
        <section className="rounded-xl border border-gray-200 bg-white p-6 shadow-sm">
          <h2 className="mb-4 text-base font-semibold text-gray-800">
            New Note
          </h2>
          <NoteForm
            submitLabel="Add Note"
            onSubmit={(data) => createNote.mutate(data)}
            isPending={createNote.isPending}
          />
        </section>

        {/* Notes list */}
        <section>
          <h2 className="mb-4 text-base font-semibold text-gray-800">
            All Notes
          </h2>
          {isLoading && (
            <p className="text-center text-gray-400 text-sm py-8">
              Loading…
            </p>
          )}
          {isError && (
            <p className="text-center text-red-500 text-sm py-8">
              Failed to load notes. Make sure the API is running.
            </p>
          )}
          {notes && <NoteList notes={notes} />}
        </section>
      </main>
    </div>
  );
}
