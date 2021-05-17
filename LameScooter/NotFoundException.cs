using System;

namespace LameScooter {
    public class NotFoundException : Exception {
        public NotFoundException(string name) : base($"'{name}' not found.") { }
    }
}