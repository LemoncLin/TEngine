"""Minimal workflow unit tests (local fill-in; upstream .codex/tests is missing on main)."""
from __future__ import annotations

import sys
import unittest
from pathlib import Path

# UnityProject/.codex/scripts 必须在 path 上，才能 import workflow_lib
ROOT = Path(__file__).resolve().parents[1]  # .codex
SCRIPTS = ROOT / "scripts"
PROJECT = ROOT.parent  # UnityProject
if str(SCRIPTS) not in sys.path:
    sys.path.insert(0, str(SCRIPTS))


class TestWorkflowLayout(unittest.TestCase):
    def test_required_skills_exist(self):
        skills = ROOT / "skills"
        expected = ("tengine-dev", "unity-cli", "luban-dev", "html-to-ugui")
        for name in expected:
            skill_md = skills / name / "SKILL.md"
            self.assertTrue(skill_md.is_file(), f"missing skill: {skill_md}")

    def test_workflow_entry_importable(self):
        import workflow_lib.checks as checks
        import workflow_lib.core as core
        self.assertTrue(hasattr(checks, "unit_tests"))
        self.assertTrue(hasattr(checks, "structure"))
        self.assertTrue(hasattr(core, "digest"))

    def test_core_digest_stable(self):
        from workflow_lib.core import digest
        self.assertEqual(digest({"a": 1}), digest({"a": 1}))
        self.assertNotEqual(digest({"a": 1}), digest({"a": 2}))

    def test_scenarios_json_present(self):
        path = ROOT / "evals" / "scenarios.json"
        self.assertTrue(path.is_file(), f"missing {path}")


if __name__ == "__main__":
    unittest.main()
