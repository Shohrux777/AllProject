<template>
  <body>
    <table>
        <tr>
            <th>Client</th>
            <th>Subject</th>
            <th>Status</th>
            <th>Priority</th>
        </tr>
        <tr>
            <td>Emma Watson</td>
            <td>Synapse Design #1125</td>
            <td><span class="status recent">Recent</span></td>
            <td><span class="priority urgent">Urgent</span></td>
        </tr>
        <tr>
            <td>Luke</td>
            <td>Change of refund my last buy | Order #125631</td>
            <td><span class="status overdue">Overdue</span></td>
            <td><span class="priority high">High</span></td>
        </tr>
        <tr>
            <td>Finley</td>
            <td>I need your help #2256</td>
            <td><span class="status remaining">Remaining</span></td>
            <td><span class="priority medium">Medium</span></td>
        </tr>
        <tr>
            <td>Peter Gill</td>
            <td>I need your help #2256</td>
            <td><span class="status responded">Responded</span></td>
            <td><span class="priority low">Low</span></td>
        </tr>
        <tr>
            <td>Freya</td>
            <td>Contact Forms #3264</td>
            <td><span class="status closed">Closed</span></td>
            <td><span class="priority medium">Medium</span></td>
        </tr>
    </table>
</body>
</template>

<script>
export default {

}
</script>

<style>
body {
            font-family: Arial, sans-serif;
            margin: 20px;
            background-color: #f9f9f9;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            background: white;
            box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            overflow: hidden;
        }
        th, td {
            border: 1px solid #ddd;
            padding: 12px;
            text-align: left;
        }
        th {
            background-color: #007BFF;
            color: white;
            text-transform: uppercase;
        }
        .status {
            padding: 5px 10px;
            border-radius: 5px;
            color: white;
            font-weight: bold;
            display: inline-block;
        }
        .recent { background-color: #28a745; }
        .overdue { background-color: #dc3545; }
        .remaining { background-color: #ffc107; color: black; }
        .responded { background-color: #17a2b8; }
        .closed { background-color: #6c757d; }
        .priority {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 5px;
            font-weight: bold;
        }
        .urgent { color: #dc3545; font-weight: bold; }
        .high { color: #fd7e14; font-weight: bold; }
        .medium { color: #007BFF; font-weight: bold; }
        .low { color: #28a745; font-weight: bold; }
        tr:hover {
            background-color: #f1f1f1;
        }
</style>