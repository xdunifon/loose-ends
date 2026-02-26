export function getDateDiffSeconds(targetDate, startDate) {
  let start = new Date()
  if (startDate) {
    start = startDate
  }

  return (targetDate - start) / 1000
}
